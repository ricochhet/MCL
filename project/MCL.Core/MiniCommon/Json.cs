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

using System.Text.Encodings.Web;
using System.Text.Json;

namespace MCL.Core.MiniCommon;

public static class Json
{
    public static JsonSerializerOptions JsonSerializerOptions { get; private set; } =
        new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    /// <summary>
    /// Serialize data of type T.
    /// </summary>
    public static string Serialize<T>(T data, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(data, options);
    }

    /// <summary>
    /// Deserialize data of type T.
    /// </summary>
    public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>

    public static void Save<T>(string filepath, T data)
    {
        string json = Serialize(data);
        VFS.WriteFile(filepath, json);
    }

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>
    public static void Save<T>(string filepath, T data, JsonSerializerOptions options)
    {
        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        VFS.WriteFile(filepath, Serialize(data, options));
    }

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public static T? Load<T>(string filepath)
        where T : new()
    {
        if (!VFS.Exists(filepath))
            return default;

        string json = VFS.ReadAllText(filepath);
        try
        {
            return Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public static T? Load<T>(string filepath, JsonSerializerOptions options)
        where T : new()
    {
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
}
