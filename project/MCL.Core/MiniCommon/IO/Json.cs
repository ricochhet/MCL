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

using System.Text.Json;
using System.Text.Json.Serialization;
using MCL.Core.MiniCommon.IO.Interfaces;

namespace MCL.Core.MiniCommon.IO;

public class Json : IJson
{
    public static BaseJson BaseJson { get; private set; } = new();

    /// <inheritdoc />
    public static string Serialize<T>(T data, JsonSerializerOptions options)
    {
        return BaseJson.Serialize(data, options);
    }

    /// <inheritdoc />
    public static string Serialize<T>(T data, JsonSerializerContext ctx)
    {
        return BaseJson.Serialize(data, ctx);
    }

    /// <inheritdoc />
    public static T? Deserialize<T>(string json, JsonSerializerOptions options)
    {
        return BaseJson.Deserialize<T>(json, options);
    }

    /// <inheritdoc />
    public static T? Deserialize<T>(string data, JsonSerializerContext ctx)
        where T : class
    {
        return BaseJson.Deserialize<T>(data, ctx);
    }

    /// <inheritdoc />
    public static void Save<T>(string filepath, T data, JsonSerializerOptions options)
    {
        BaseJson.Save(filepath, data, options);
    }

    /// <inheritdoc />
    public static void Save<T>(string filepath, T data, JsonSerializerContext ctx)
    {
        BaseJson.Save(filepath, data, ctx);
    }

    /// <inheritdoc />
    public static T? Load<T>(string filepath, JsonSerializerOptions options)
        where T : new()
    {
        return BaseJson.Load<T>(filepath, options);
    }

    /// <inheritdoc />
    public static T? Load<T>(string filepath, JsonSerializerContext ctx)
        where T : class
    {
        return BaseJson.Load<T>(filepath, ctx);
    }
}
