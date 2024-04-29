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

using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.MiniCommon.Web.Abstractions;

public class ValidatedRequest : BaseRequest
{
    /// <inheritdoc />
    public override async Task<HttpResponseMessage?> GetAsync(string request)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request], NativeLogLevel.Fatal))
            return null;
        return await base.GetAsync(request);
    }

    /// <inheritdoc />
    public override async Task<byte[]?> GetByteArrayAsync(string request)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request], NativeLogLevel.Fatal))
            return null;
        return await base.GetByteArrayAsync(request);
    }

    /// <inheritdoc />
    public override async Task<Stream?> GetStreamAsync(string request)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request], NativeLogLevel.Fatal))
            return null;
        return await base.GetStreamAsync(request);
    }

    /// <inheritdoc />
    public override async Task<T?> GetObjectFromJsonAsync<T>(string request)
        where T : struct
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request], NativeLogLevel.Fatal))
            return null;
        return await base.GetObjectFromJsonAsync<T>(request);
    }

    /// <inheritdoc />
    public override async Task<string?> GetJsonAsync<T>(string request, string filepath, Encoding encoding)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request, filepath], NativeLogLevel.Fatal))
            return null;
        return await base.GetJsonAsync<T>(request, filepath, encoding);
    }

    /// <inheritdoc />
    public override async Task<string?> GetStringAsync(string request, string filepath, Encoding encoding)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request, filepath], NativeLogLevel.Fatal))
            return null;
        return await base.GetStringAsync(request, filepath, encoding);
    }

    /// <inheritdoc />
    public override async Task<string?> GetStringAsync(string request)
    {
        return await base.GetStringAsync(request);
    }

    /// <inheritdoc />
    public override async Task<bool> DownloadSHA256(string request, string filepath, string hash)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request, filepath], NativeLogLevel.Fatal))
            return false;
        return await base.DownloadSHA256(request, filepath, hash);
    }

    /// <inheritdoc />
    public override async Task<bool> DownloadSHA1(string request, string filepath, string hash)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request, filepath], NativeLogLevel.Fatal))
            return false;
        return await base.DownloadSHA1(request, filepath, hash);
    }

    /// <inheritdoc />
    public override async Task<bool> Download(string request, string filepath)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([request, filepath], NativeLogLevel.Fatal))
            return false;
        return await base.Download(request, filepath);
    }
}
