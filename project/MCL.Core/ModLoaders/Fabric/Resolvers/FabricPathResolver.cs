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

using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Resolvers;

public static class FabricPathResolver
{
    /// <summary>
    /// The Fabric mod path.
    /// </summary>
    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.FabricPath, "mods");

    /// <summary>
    /// The Fabric mod version path specified by the MVersion.
    /// </summary>
    public static string ModVersionPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.MVersion);

    /// <summary>
    /// The Fabric installer jar path specified by the FabricInstallerVersion.
    /// </summary>
    public static string InstallerPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.FabricPath ?? ValidationShims.StringEmpty(),
            "installers",
            $"fabric-installer-{launcherVersion?.FabricInstallerVersion}.jar"
        );

    /// <summary>
    /// The base installer path for Fabric installers.
    /// </summary>
    public static string InstallersPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.FabricPath ?? ValidationShims.StringEmpty(), "installers");

    /// <summary>
    /// The Fabric manifest path.
    /// </summary>
    public static string VersionManifestPath(LauncherPath? launcherPath) =>
        VFS.FromCwd(launcherPath?.FabricPath ?? ValidationShims.StringEmpty(), "fabric_manifest.json");

    /// <summary>
    /// The Fabric profile path specified by the FabricLoaderVersion.
    /// </summary>
    public static string ProfilePath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.FromCwd(
            launcherPath?.FabricPath ?? ValidationShims.StringEmpty(),
            $"fabric_profile-{launcherVersion?.MVersion}-{launcherVersion?.FabricLoaderVersion}.json"
        );

    /// <summary>
    /// The Fabric loader jar path specified by the FabricLoaderVersion.
    /// </summary>
    public static string LoaderJarPath(FabricUrls? fabricUrls, LauncherVersion? launcherVersion) =>
        fabricUrls?.LoaderJar.Replace("{0}", launcherVersion?.FabricLoaderVersion) ?? ValidationShims.StringEmpty();

    /// <summary>
    /// The Fabric loader profile path specified by the FabricLoaderVersion.
    /// </summary>
    public static string LoaderProfilePath(FabricUrls? fabricUrls, LauncherVersion? launcherVersion) =>
        string.Format(
            fabricUrls?.LoaderProfile ?? ValidationShims.StringEmpty(),
            launcherVersion?.MVersion ?? ValidationShims.StringEmpty(),
            launcherVersion?.FabricLoaderVersion ?? ValidationShims.StringEmpty()
        );
}
