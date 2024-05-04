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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MiniCommon.Cryptography.Helpers;
using MiniCommon.IO;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;
using MiniCommon.Web.Interfaces;

namespace MiniCommon.Web.Abstractions;

public class BaseRequest : IBaseHttpRequest
{
    private static readonly HttpClient _httpClient = new();

    public virtual HttpClient GetHttpClient() => _httpClient;

    public virtual int Retry { get; set; } = 1;

    public virtual TimeSpan HttpClientTimeOut
    {
        get { return _httpClient.Timeout; }
        set { _httpClient.Timeout = value; }
    }

    /// <inheritdoc />
    public virtual async Task<HttpResponseMessage?> GetAsync(string request)
    {
        try
        {
            return await _httpClient.GetAsync(request);
        }
        catch (Exception ex)
        {
            NotificationProvider.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <inheritdoc />
    public virtual async Task<byte[]?> GetByteArrayAsync(string request)
    {
        try
        {
            return await _httpClient.GetByteArrayAsync(request);
        }
        catch (Exception ex)
        {
            NotificationProvider.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <inheritdoc />
    public virtual async Task<Stream?> GetStreamAsync(string request)
    {
        try
        {
            return await _httpClient.GetStreamAsync(request);
        }
        catch (Exception ex)
        {
            NotificationProvider.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetObjectFromJsonAsync<T>(
        string request,
        JsonSerializerContext ctx
    )
        where T : struct
    {
        try
        {
            return await _httpClient.GetFromJsonAsync(request, typeof(T), ctx) as T?;
        }
        catch (Exception ex)
        {
            NotificationProvider.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
            );
            return default;
        }
    }

    /// <inheritdoc />
    public virtual async Task<string?> GetJsonAsync<T>(
        string request,
        string filepath,
        Encoding encoding,
        JsonSerializerContext ctx
    )
        where T : class
    {
        Stopwatch sw = Stopwatch.StartNew();
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationProvider.Info("request.get.start", request);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request) ?? Validate.For.EmptyString();
                if (Validate.For.IsNullOrWhiteSpace([response]))
                {
                    NotificationProvider.Error("error.download", request);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataProvider.Add(
                    request,
                    filepath,
                    encoding.GetByteCount(response),
                    hash,
                    sw.Elapsed
                );
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationProvider.Info("request.get.exists", request);
                    return response;
                }

                Json.Save(filepath, Json.Deserialize<T>(response, ctx), ctx);
                return response;
            }
            catch (Exception ex)
            {
                NotificationProvider.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
                );
            }
        }

        return default;
    }

    /// <inheritdoc />
    public virtual async Task<string?> GetStringAsync(
        string request,
        string filepath,
        Encoding encoding
    )
    {
        Stopwatch sw = Stopwatch.StartNew();
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationProvider.Info("request.get.start", request);
            string response;
            string hash;
            try
            {
                response = await GetStringAsync(request) ?? Validate.For.EmptyString();
                if (Validate.For.IsNullOrWhiteSpace([response]))
                {
                    NotificationProvider.Error("error.download", request);
                    return default;
                }
                hash = CryptographyHelper.CreateSHA1(response, encoding);
                RequestDataProvider.Add(
                    request,
                    filepath,
                    encoding.GetByteCount(response),
                    hash,
                    sw.Elapsed
                );
                if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
                {
                    NotificationProvider.Info("request.get.exists", request);
                    return response;
                }

                VFS.WriteFile(filepath, response);
                return response;
            }
            catch (Exception ex)
            {
                NotificationProvider.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
                );
            }
        }

        return default;
    }

    /// <inheritdoc />
    public virtual async Task<string?> GetStringAsync(string request)
    {
        try
        {
            return await _httpClient.GetStringAsync(request);
        }
        catch (Exception ex)
        {
            NotificationProvider.Error(
                "error.request",
                request,
                ex.Message,
                ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
            );
            return null;
        }
    }

    /// <inheritdoc />
    public virtual async Task<bool> DownloadSHA256(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA256(filepath, true) == hash)
        {
            RequestDataProvider.Add(request, filepath, 0, hash, TimeSpan.Zero);
            NotificationProvider.Info("request.get.exists", request);
            return true;
        }
        else if (!await Download(request, filepath))
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public virtual async Task<bool> DownloadSHA1(string request, string filepath, string hash)
    {
        if (VFS.Exists(filepath) && CryptographyHelper.CreateSHA1(filepath, true) == hash)
        {
            RequestDataProvider.Add(request, filepath, 0, hash, TimeSpan.Zero);
            NotificationProvider.Info("request.get.exists", request);
            return true;
        }
        else if (!await Download(request, filepath))
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public virtual async Task<bool> Download(string request, string filepath)
    {
        Stopwatch sw = Stopwatch.StartNew();
        for (int retry = 0; retry < Math.Max(1, Retry); retry++)
        {
            NotificationProvider.Info("request.get.start", request);
            HttpResponseMessage response;
            try
            {
                response = await GetAsync(request) ?? new(HttpStatusCode.BadRequest);

                if (Validate.For.IsNull(response))
                    return false;

                if (!response.IsSuccessStatusCode)
                    return false;

                if (!VFS.Exists(filepath))
                    VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

                RequestDataProvider.Add(request, filepath, 0, string.Empty, sw.Elapsed);
                await using Stream contentStream = await response.Content.ReadAsStreamAsync();
                await using FileStream fileStream =
                    new(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
                await contentStream.CopyToAsync(fileStream);
                return true;
            }
            catch (Exception ex)
            {
                NotificationProvider.Error(
                    "error.request",
                    request,
                    ex.Message,
                    ex.StackTrace ?? LocalizationProvider.Translate("stack.trace.null")
                );
            }
        }

        return false;
    }
}
