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

namespace MCL.Core.Minecraft.Models;

public class MAssetIndex(string id, string sha1, int size, int totalSize, string url)
{
    [JsonPropertyName("id")]
    public string ID { get; set; } = id;

    [JsonPropertyName("sha1")]
    public string SHA1 { get; set; } = sha1;

    [JsonPropertyName("size")]
    public int Size { get; set; } = size;

    [JsonPropertyName("totalSize")]
    public int TotalSize { get; set; } = totalSize;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MAssetIndex))]
internal partial class MAssetIndexContext : JsonSerializerContext;
