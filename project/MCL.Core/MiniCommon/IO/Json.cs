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

namespace MCL.Core.MiniCommon.IO;

public static class Json
{
    /// <summary>
    /// Serialize data of type T from serializer context.
    /// </summary>
    public static string Serialize<T>(T data, JsonSerializerContext ctx)
    {
        return JsonSerializer.Serialize(data!, typeof(T), ctx);
    }

    /// <summary>
    /// Deserialize data of type T from serializer context.
    /// </summary>
    public static T? Deserialize<T>(string data, JsonSerializerContext ctx)
        where T : class
    {
        return JsonSerializer.Deserialize(data!, typeof(T), ctx) as T;
    }

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>

    public static void Save<T>(string filepath, T data, JsonSerializerContext ctx)
    {
        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        string json = Serialize(data, ctx);
        VFS.WriteFile(filepath, json);
    }

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public static T? Load<T>(string filepath, JsonSerializerContext ctx)
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
