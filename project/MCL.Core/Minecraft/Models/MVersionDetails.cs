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

public class MVersionDetails
{
    [JsonPropertyName("arguments")]
    public MArgument Arguments { get; set; }

    [JsonPropertyName("assetIndex")]
    public MAssetIndex AssetIndex { get; set; }

    [JsonPropertyName("assets")]
    public string Assets { get; set; }

    [JsonPropertyName("complianceLevel")]
    public int ComplianceLevel { get; set; }

    [JsonPropertyName("downloads")]
    public MDownloads Downloads { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("javaVersion")]
    public MJavaVersion JavaVersion { get; set; }

    [JsonPropertyName("libraries")]
    public List<MLibrary> Libraries { get; set; }

    [JsonPropertyName("logging")]
    public MLogging Logging { get; set; }

    [JsonPropertyName("mainClass")]
    public string MainClass { get; set; }

    [JsonPropertyName("minimumLauncherVersion")]
    public int MinimumLauncherVersion { get; set; }

    [JsonPropertyName("releaseTime")]
    public string ReleaseTime { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    public MVersionDetails() { }
}
