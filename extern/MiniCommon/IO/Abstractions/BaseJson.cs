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

using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MiniCommon.BuildInfo;
using MiniCommon.IO.Interfaces;

namespace MiniCommon.IO.Abstractions;

public class BaseJson : IBaseJson
{
    /// <inheritdoc />
    public virtual string Serialize<T>(T data, JsonSerializerOptions options)
    {
        if (AotConstants.IsNativeAot)
            throw new SerializationException();
#pragma warning disable IL2026, IL3050
        return JsonSerializer.Serialize(data, options);
#pragma warning restore IL2026, IL3050
    }

    /// <inheritdoc />
    public virtual string Serialize<T>(T data, JsonSerializerContext ctx)
    {
        return JsonSerializer.Serialize(data, typeof(T), ctx);
    }

    /// <inheritdoc />
    public virtual T? Deserialize<T>(string json, JsonSerializerOptions options)
    {
        if (AotConstants.IsNativeAot)
            throw new SerializationException();
#pragma warning disable IL2026, IL3050
        return JsonSerializer.Deserialize<T>(json, options);
#pragma warning restore IL2026, IL3050
    }

    /// <inheritdoc />
    public virtual T? Deserialize<T>(string data, JsonSerializerContext ctx)
        where T : class
    {
        return JsonSerializer.Deserialize(data!, typeof(T), ctx) as T;
    }

    /// <inheritdoc />
    public virtual void Save<T>(string filepath, T data, JsonSerializerOptions options)
    {
        if (AotConstants.IsNativeAot)
            throw new SerializationException();

        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        string json = Serialize(data, options);
        VFS.WriteFile(filepath, json);
    }

    /// <inheritdoc />
    public virtual void Save<T>(string filepath, T data, JsonSerializerContext ctx)
    {
        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        string json = Serialize(data, ctx);
        VFS.WriteFile(filepath, json);
    }

    /// <inheritdoc />
    public virtual T? Load<T>(string filepath, JsonSerializerOptions options)
        where T : new()
    {
        if (AotConstants.IsNativeAot)
            throw new SerializationException();

        if (!VFS.Exists(filepath))
            return default;

        string json = VFS.ReadAllText(filepath);
        try
        {
            return Deserialize<T>(json, options);
        }
        catch
        {
            return default;
        }
    }

    /// <inheritdoc />
    public virtual T? Load<T>(string filepath, JsonSerializerContext ctx)
        where T : class
    {
        if (!VFS.Exists(filepath))
            return default;

        string json = VFS.ReadAllText(filepath);
        try
        {
            return Deserialize<T>(json, ctx);
        }
        catch
        {
            return default;
        }
    }
}
