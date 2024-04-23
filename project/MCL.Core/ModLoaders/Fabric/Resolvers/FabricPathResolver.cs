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
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Resolvers;

public static class FabricPathResolver
{
    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.FabricPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.MVersion);

    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.FabricPath,
            "installers",
            $"fabric-installer-{launcherVersion.FabricInstallerVersion}.jar"
        );

    public static string InstallersPath(LauncherPath launcherPath) =>
        VFS.Combine(launcherPath.FabricPath, "installers");

    public static string VersionManifestPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricPath, "fabric_manifest.json");

    public static string ProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricPath,
            $"fabric_profile-{launcherVersion.MVersion}-{launcherVersion.FabricLoaderVersion}.json"
        );

    public static string LoaderJarPath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        fabricUrls.LoaderJar.Replace("{0}", launcherVersion.FabricLoaderVersion);

    public static string LoaderProfilePath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        string.Format(fabricUrls.LoaderProfile, launcherVersion.MVersion, launcherVersion.FabricLoaderVersion);
}
