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

namespace MCL.Core.Launcher.Models;

public class LauncherVersion
{
    public string Version { get; set; } = string.Empty;
    public string VersionType { get; set; } = "release";
    public string FabricInstallerVersion { get; set; } = string.Empty;
    public string FabricLoaderVersion { get; set; } = string.Empty;
    public string QuiltInstallerVersion { get; set; } = string.Empty;
    public string QuiltLoaderVersion { get; set; } = string.Empty;
    public string PaperServerVersion { get; set; } = string.Empty;

    public LauncherVersion() { }

    public LauncherVersion(
        string version,
        string versionType,
        string fabricInstallerVersion,
        string fabricLoaderVersion,
        string quiltInstallerVersion,
        string quiltLoaderVersion,
        string paperServerVersion
    )
    {
        Version = version;
        VersionType = versionType;
        FabricInstallerVersion = fabricInstallerVersion;
        FabricLoaderVersion = fabricLoaderVersion;
        QuiltInstallerVersion = quiltInstallerVersion;
        QuiltLoaderVersion = quiltLoaderVersion;
        PaperServerVersion = paperServerVersion;
    }

    public static LauncherVersion Latest() =>
        new()
        {
            Version = "latest",
            VersionType = "release",
            FabricInstallerVersion = "latest",
            FabricLoaderVersion = "latest",
            QuiltInstallerVersion = "latest",
            QuiltLoaderVersion = "latest",
            PaperServerVersion = "latest"
        };
}
