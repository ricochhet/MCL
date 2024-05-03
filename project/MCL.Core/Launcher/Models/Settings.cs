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
using MCL.Core.FileExtractors.Models;
using MCL.Core.Java.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Modding.Models;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.Servers.Paper.Models;

namespace MCL.Core.Launcher.Models;

public class Settings
{
    public MainClassNames? MainClassNames { get; set; }
    public LauncherMemory? LauncherMemory { get; set; }

    public LauncherInstance? LauncherInstance { get; set; }

    [JsonPropertyName("LauncherInstance.Override")]
    public LauncherInstance? OverrideLauncherInstance { get; set; }

    public LauncherUsername? LauncherUsername { get; set; }
    public LauncherPath? LauncherPath { get; set; }
    public LauncherVersion? LauncherVersion { get; set; }
    public LauncherSettings? LauncherSettings { get; set; }
    public MUrls? MUrls { get; set; }
    public FabricUrls? FabricUrls { get; set; }
    public QuiltUrls? QuiltUrls { get; set; }
    public PaperUrls? PaperUrls { get; set; }

    public JvmArguments? MJvmArguments { get; set; }

    [JsonPropertyName("MJvmArguments.Override")]
    public JvmArguments? OverrideMJvmArguments { get; set; }

    public JvmArguments? FabricJvmArguments { get; set; }

    [JsonPropertyName("FabricJvmArguments.Override")]
    public JvmArguments? OverrideFabricJvmArguments { get; set; }

    public JvmArguments? QuiltJvmArguments { get; set; }

    [JsonPropertyName("QuiltJvmArguments.Override")]
    public JvmArguments? OverrideQuiltJvmArguments { get; set; }

    public JvmArguments? PaperJvmArguments { get; set; }

    [JsonPropertyName("PaperJvmArguments.Override")]
    public JvmArguments? OverridePaperJvmArguments { get; set; }

    public JavaSettings? JavaSettings { get; set; }
    public SevenZipSettings? SevenZipSettings { get; set; }

    public ModSettings? ModSettings { get; set; }

    [JsonPropertyName("ModSettings.Override")]
    public ModSettings? OverrideModSettings { get; set; }

    public Settings() { }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Settings))]
internal partial class SettingsContext : JsonSerializerContext;
