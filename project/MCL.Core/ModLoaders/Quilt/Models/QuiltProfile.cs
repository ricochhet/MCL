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

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using MCL.Core.Minecraft.Models;

namespace MCL.Core.ModLoaders.Quilt.Models;

public class QuiltProfile
{
    [JsonPropertyName("id")]
    public string? ID { get; set; }

    [JsonPropertyName("inheritsFrom")]
    public string? InheritsFrom { get; set; }

    [JsonPropertyName("releaseTime")]
    public string? ReleaseTime { get; set; }

    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("mainClass")]
    public string? MainClass { get; set; }

    [JsonPropertyName("arguments")]
    public MArgument? Arguments { get; set; }

    [JsonPropertyName("libraries")]
    public List<QuiltLibrary>? Libraries { get; set; }

    public QuiltProfile() { }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(QuiltProfile))]
[JsonSerializable(typeof(JsonElement))]
internal partial class QuiltProfileContext : JsonSerializerContext;
