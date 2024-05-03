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

public class MVersion(string id, string type, string url, string time, string releaseTime)
{
    [JsonPropertyName("id")]
    public string ID { get; set; } = id;

    [JsonPropertyName("type")]
    public string Type { get; set; } = type;

    [JsonPropertyName("url")]
    public string URL { get; set; } = url;

    [JsonPropertyName("time")]
    public string Time { get; set; } = time;

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; } = releaseTime;
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(MVersion))]
internal partial class MVersionContext : JsonSerializerContext;
