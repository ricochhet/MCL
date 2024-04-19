using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon.Helpers;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon;

public static class Request
{
    private static readonly HttpClient _httpClient = new();
    public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new();

    public static HttpClient GetHttpClient() => _httpClient;

    public static int Retry { get; set; } = 1;

    public static TimeSpan HttpClientTimeOut
    {
        get { return _httpClient.Timeout; }
        set { _httpClient.Timeout = value; }
    }

#nullable enable
    public static async Task<HttpResponseMessage?> GetAsync(string request)
    {
        try
        {
            return await _httpClient.GetAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.request",
                [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
            return null;
        }
    }

    public static async Task<byte[]?> GetByteArrayAsync(string request)
    {
        try
        {
            return await _httpClient.GetByteArrayAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.request",
                [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
            return null;
        }
    }

    public static async Task<Stream?> GetStreamAsync(string request)
    {
        try
        {
            return await _httpClient.GetStreamAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.request",
                [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
            return null;
        }
    }

    public static async Task<T?> GetObjectFromJsonAsync<T>(string request)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<T>(request);
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.request",
                [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
            return default;
        }
    }

#nullable disable

    public static async Task<string> GetJsonAsync<T>(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationService.Log(NativeLogLevel.Info, "request.get.start", [request]);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request);
                if (string.IsNullOrWhiteSpace(response))
                {
                    NotificationService.Log(NativeLogLevel.Error, "error.download", [request]);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(request, filepath, encoding.GetByteCount(response), hash);
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Log(NativeLogLevel.Info, "request.get.exists", [request]);
                    return response;
                }

                Json.Save(filepath, Json.Deserialize<T>(response), JsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Log(
                    NativeLogLevel.Error,
                    "error.request",
                    [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                );
            }
        }

        return default;
    }

    public static async Task<string> GetStringAsync(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationService.Log(NativeLogLevel.Info, "request.get.start", [request]);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request);
                if (string.IsNullOrWhiteSpace(response))
                {
                    NotificationService.Log(NativeLogLevel.Error, "error.download", [request]);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(request, filepath, encoding.GetByteCount(response), hash);
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Log(NativeLogLevel.Info, "request.get.exists", [request]);
                    return response;
                }

                VFS.WriteFile(filepath, response);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Log(
                    NativeLogLevel.Error,
                    "error.request",
                    [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                );
            }
        }

        return default;
    }

#nullable enable
    public static async Task<string?> GetStringAsync(string request)
    {
        try
        {
            return await _httpClient.GetStringAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.request",
                [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
            return null;
        }
    }

#nullable disable

    public static async Task<bool> Download(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
        {
            RequestDataService.Add(request, filepath, 0, hash);
            NotificationService.Log(NativeLogLevel.Info, "request.get.exists", [request]);
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
            NotificationService.Log(NativeLogLevel.Info, "request.get.start", [request]);
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

                RequestDataService.Add(request, filepath, 0, string.Empty);
                using Stream contentStream = await response.Content.ReadAsStreamAsync();
                using FileStream fileStream = new(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
                await contentStream.CopyToAsync(fileStream);
                return true;
            }
            catch (Exception ex)
            {
                NotificationService.Log(
                    NativeLogLevel.Error,
                    "error.request",
                    [request, ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                );
            }
        }

        return false;
    }
}
