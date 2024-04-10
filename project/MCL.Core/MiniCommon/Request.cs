using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.Logger;

namespace MCL.Core.MiniCommon;

public static class Request
{
    private static readonly HttpClient httpClient = new();
    private static JsonSerializerOptions JsonSerializerOptions = new();

    public static HttpClient GetHttpClient() => httpClient;

    public static JsonSerializerOptions GetJsonSerializerOptions() => JsonSerializerOptions;

    public static void SetJsonSerializerOptions(JsonSerializerOptions options) => JsonSerializerOptions = options;

#nullable enable
    public static async Task<HttpResponseMessage?> GetAsync(string request)
    {
        try
        {
            LogBase.Debug($"GET: {request}");
            return await httpClient.GetAsync(request);
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return null;
        }
    }

    public static async Task<string?> GetStringAsync(string request)
    {
        try
        {
            LogBase.Debug($"GET: {request}");
            return await httpClient.GetStringAsync(request);
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return null;
        }
    }

    public static async Task<T?> GetObjectFromJsonAsync<T>(string request)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<T>(request);
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return default;
        }
    }

#nullable disable

    public static async Task<string> GetJsonAsync<T>(string request, string filepath, Encoding encoding)
    {
        try
        {
            string response = await GetStringAsync(request);
            if (VFS.Exists(filepath))
            {
                if (CryptographyHelper.Sha1(filepath, true) == CryptographyHelper.Sha1(response, encoding))
                    return response;
            }

            Json.Save(filepath, Json.Deserialize<T>(response), JsonSerializerOptions);
            return response;
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return default;
        }
    }

    public static async Task<string> GetStringAsync(string request, string filepath, Encoding encoding)
    {
        try
        {
            string response = await GetStringAsync(request);
            if (VFS.Exists(filepath))
            {
                if (CryptographyHelper.Sha1(filepath, true) == CryptographyHelper.Sha1(response, encoding))
                    return response;
            }

            VFS.WriteFile(filepath, response);
            return response;
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return default;
        }
    }

    public static async Task<bool> Download(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.Sha1(filepath, true) == hash)
            return true;
        else if (!await Download(request, filepath))
            return false;
        return true;
    }

    public static async Task<bool> Download(string request, string filepath)
    {
        try
        {
#nullable enable
            HttpResponseMessage? response = await GetAsync(request);
#nullable disable

            if (response == null)
                return false;

            if (!response.IsSuccessStatusCode)
                return false;

            if (!VFS.Exists(filepath))
                VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

            using Stream contentStream = await response.Content.ReadAsStreamAsync();
            using FileStream fileStream = new(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
            await contentStream.CopyToAsync(fileStream);
            return true;
        }
        catch (Exception ex)
        {
            LogBase.Error(ex.ToString());
            return false;
        }
    }
}
