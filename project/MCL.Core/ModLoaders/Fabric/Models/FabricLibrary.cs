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

using System.Text.Json.Serialization;

namespace MCL.Core.ModLoaders.Fabric.Models;

public class FabricLibrary(string name, string url, string md5, string sha1, string sha256, string sha512, int size)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

    [JsonPropertyName("md5")]
    public string MD5 { get; set; } = md5;

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; } = sha1;

    [JsonPropertyName("sha256")]
    public string SHA256 { get; set; } = sha256;

    [JsonPropertyName("sha512")]
    public string SHA512 { get; set; } = sha512;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;

    public static string ParseURL(string name, string url)
    {
        string path;
        string[] parts = name.Split(":", 3);
        path = parts[0].Replace(".", "/") + "/" + parts[1] + "/" + parts[2] + "/" + parts[1] + "-" + parts[2] + ".jar";
        return url + path;
    }

    public static string ParsePath(string name)
    {
        string[] parts = name.Split(":", 3);
        char separator = '/';
        string path =
            parts[0].Replace('.', separator)
            + separator
            + parts[1]
            + separator
            + parts[2]
            + separator
            + parts[1]
            + "-"
            + parts[2]
            + ".jar";
        return path.Replace(" ", "_");
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(FabricLibrary))]
internal partial class FabricLibraryContext : JsonSerializerContext { }
