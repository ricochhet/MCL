/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.MiniCommon.Helpers;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon;

public static class Request
{
    private static readonly HttpClient _httpClient = new();
    public static JsonSerializerOptions JsonSerializerOptions { get; set; } = Json.JsonSerializerOptions;

    public static HttpClient GetHttpClient() => _httpClient;

    public static int Retry { get; set; } = 1;

    public static TimeSpan HttpClientTimeOut
    {
        get { return _httpClient.Timeout; }
        set { _httpClient.Timeout = value; }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI.
    /// </summary>
    public static async Task<HttpResponseMessage?> GetAsync(string request)
    {
        try
        {
            return await _httpClient.GetAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a byte array.
    /// </summary>
    public static async Task<byte[]?> GetByteArrayAsync(string request)
    {
        try
        {
            return await _httpClient.GetByteArrayAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a stream.
    /// </summary>
    public static async Task<Stream?> GetStreamAsync(string request)
    {
        try
        {
            return await _httpClient.GetStreamAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a deserialized object of type T.
    /// </summary>
    public static async Task<T?> GetObjectFromJsonAsync<T>(string request)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<T>(request);
        }
        catch (Exception ex)
        {
            NotificationService.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
            return default;
        }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the deserialized response of type T to a file.
    /// </summary>
    public static async Task<string?> GetJsonAsync<T>(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationService.Info("request.get.start", request);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request) ?? ValidationShims.StringEmpty();
                if (ObjectValidator<string>.IsNullOrWhiteSpace([response]))
                {
                    NotificationService.Error("error.download", request);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(request, filepath, encoding.GetByteCount(response), hash);
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Info("request.get.exists", request);
                    return response;
                }

                Json.Save(filepath, Json.Deserialize<T>(response), JsonSerializerOptions);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
                );
            }
        }

        return default;
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response to a file.
    /// </summary>
    public static async Task<string?> GetStringAsync(string request, string filepath, Encoding encoding)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationService.Info("request.get.start", request);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request) ?? ValidationShims.StringEmpty();
                if (ObjectValidator<string>.IsNullOrWhiteSpace([response]))
                {
                    NotificationService.Error("error.download", request);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataService.Add(request, filepath, encoding.GetByteCount(response), hash);
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationService.Info("request.get.exists", request);
                    return response;
                }

                VFS.WriteFile(filepath, response);
                return response;
            }
            catch (Exception ex)
            {
                NotificationService.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
                );
            }
        }

        return default;
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a string.
    /// </summary>
    public static async Task<string?> GetStringAsync(string request)
    {
        try
        {
            return await _httpClient.GetStringAsync(request);
        }
        catch (Exception ex)
        {
            NotificationService.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA256 hashes, and saves file if comparison is false.
    /// </summary>
    public static async Task<bool> DownloadSHA256(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA256(filepath, true) == hash)
        {
            RequestDataService.Add(request, filepath, 0, hash);
            NotificationService.Info("request.get.exists", request);
            return true;
        }
        else if (!await Download(request, filepath))
            return false;
        return true;
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA1 hashes, and saves file if comparison is false.
    /// </summary>
    public static async Task<bool> DownloadSHA1(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
        {
            RequestDataService.Add(request, filepath, 0, hash);
            NotificationService.Info("request.get.exists", request);
            return true;
        }
        else if (!await Download(request, filepath))
            return false;
        return true;
    }

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response as a filestream.
    /// </summary>
    public static async Task<bool> Download(string request, string filepath)
    {
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationService.Info("request.get.start", request);
            HttpResponseMessage response;
            try
            {
                response = await GetAsync(request) ?? new(HttpStatusCode.BadRequest);

                if (ObjectValidator<HttpResponseMessage>.IsNull(response))
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
                NotificationService.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
                );
            }
        }

        return false;
    }
}
