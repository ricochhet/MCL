using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.Logger.Enums;
using MCL.Core.Models.Services;
using MCL.Core.Models.Web;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Web;

namespace MCL.Core.MiniCommon;

public static class Request
{
    private static readonly HttpClient httpClient = new();
    public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new();

    public static HttpClient GetHttpClient() => httpClient;

    public static int Retry { get; set; } = 1;

    public static TimeSpan HttpClientTimeOut
    {
        get { return httpClient.Timeout; }
        set { httpClient.Timeout = value; }
    }

#nullable enable
    public static async Task<HttpResponseMessage?> GetAsync(string request)
    {
        try
        {
            return await httpClient.GetAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
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
            NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
            return default;
        }
    }

#nullable disable

    public static async Task<string> GetJsonAsync<T>(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request);
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(new RequestData(request, filepath, encoding.GetByteCount(response), hash));
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Add(
                        new Notification(NativeLogLevel.Info, "request.get.hash-exists", [request])
                    );
                    return response;
                }

                Json.Save(filepath, Json.Deserialize<T>(response), JsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
            }
        }

        return default;
    }

    public static async Task<string> GetStringAsync(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request);
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(new RequestData(request, filepath, encoding.GetByteCount(response), hash));
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Add(
                        new Notification(NativeLogLevel.Info, "request.get.hash-exists", [request])
                    );
                    return response;
                }

                VFS.WriteFile(filepath, response);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
            }
        }

        return default;
    }

#nullable enable
    public static async Task<string?> GetStringAsync(string request)
    {
        try
        {
            return await httpClient.GetStringAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
            return null;
        }
    }

#nullable disable

    public static async Task<bool> Download(string request, string filepath, string hash)
    {
        RequestDataService.Add(new RequestData(request, filepath, 0, hash));
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Info, "request.get.hash-exists", [request]));
            return true;
        }
        else if (!await Download(request, filepath))
            return false;
        return true;
    }

    public static async Task<bool> Download(string request, string filepath)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
#nullable enable
            HttpResponseMessage? response;
#nullable disable
            try
            {
                response = await GetAsync(request);

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
                NotificationService.Add(new Notification(NativeLogLevel.Error, "log", [ex.Message]));
            }
        }

        return false;
    }
}
