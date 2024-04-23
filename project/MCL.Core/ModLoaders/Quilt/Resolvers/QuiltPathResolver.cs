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
using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public static class QuiltPathResolver
{
    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.QuiltPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.MVersion);

    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltPath,
            "installers",
            $"quilt-installer-{launcherVersion.QuiltInstallerVersion}.jar"
        );

    public static string InstallersPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.QuiltPath, "installers");

    public static string VersionManifestPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltPath, "quilt_manifest.json");

    public static string ProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.QuiltPath,
            $"quilt_profile-{launcherVersion.MVersion}-{launcherVersion.QuiltLoaderVersion}.json"
        );

    public static string LoaderJarPath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        quiltUrls.LoaderJar.Replace("{0}", launcherVersion.QuiltLoaderVersion);

    public static string LoaderProfilePath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        string.Format(quiltUrls.LoaderProfile, launcherVersion.MVersion, launcherVersion.QuiltLoaderVersion);
}
