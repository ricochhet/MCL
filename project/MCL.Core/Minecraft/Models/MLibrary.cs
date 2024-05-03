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
using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLibrary(string name, MLibraryDownloads downloads, List<MLibraryRule> rules, MLibraryNatives natives)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("downloads")]
    public MLibraryDownloads Downloads { get; set; } = downloads;

    [JsonPropertyName("rules")]
    public List<MLibraryRule> Rules { get; set; } = rules;

    [JsonPropertyName("natives")]
    public MLibraryNatives? Natives { get; set; } = natives;
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MLibrary))]
internal partial class MLibraryContext : JsonSerializerContext;
