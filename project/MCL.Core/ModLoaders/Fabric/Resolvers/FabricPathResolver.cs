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
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Interfaces.Resolvers;
using MiniCommon.IO;
using MiniCommon.Validation.Operators;

namespace MCL.Core.ModLoaders.Fabric.Resolvers;

public class FabricPathResolver : IModLoaderPathResolver<FabricUrls>
{
    /// <inheritdoc />
    public static string ModPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.FabricPath, "mods");

    /// <inheritdoc />
    public static string ModVersionPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.MVersion);

    /// <inheritdoc />
    public static string InstallerPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.FabricPath ?? StringOperator.Empty(),
            "installers",
            $"fabric-installer-{launcherVersion?.FabricInstallerVersion}.jar"
        );

    /// <inheritdoc />
    public static string InstallersPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.FabricPath ?? StringOperator.Empty(), "installers");

    /// <inheritdoc />
    public static string VersionManifestPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.FabricPath ?? StringOperator.Empty(), "fabric_manifest.json");

    /// <inheritdoc />
    public static string ProfilePath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.FabricPath ?? StringOperator.Empty(),
            $"fabric_profile-{launcherVersion?.MVersion}-{launcherVersion?.FabricLoaderVersion}.json"
        );

    /// <inheritdoc />
    public static string LoaderJarPath(FabricUrls? fabricUrls, LauncherVersion? launcherVersion) =>
        fabricUrls?.LoaderJar.Replace("{0}", launcherVersion?.FabricLoaderVersion) ?? StringOperator.Empty();

    /// <inheritdoc />
    public static string LoaderProfilePath(FabricUrls? fabricUrls, LauncherVersion? launcherVersion) =>
        string.Format(
            fabricUrls?.LoaderProfile ?? StringOperator.Empty(),
            launcherVersion?.MVersion ?? StringOperator.Empty(),
            launcherVersion?.FabricLoaderVersion ?? StringOperator.Empty()
        );
}
