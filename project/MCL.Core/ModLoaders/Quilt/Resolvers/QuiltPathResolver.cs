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
using MCL.Core.ModLoaders.Interfaces.Resolvers;
using MCL.Core.ModLoaders.Quilt.Models;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public class QuiltPathResolver : IModLoaderPathResolver<QuiltUrls>
{
    /// <inheritdoc />
    public static string ModPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.QuiltPath, "mods");

    /// <inheritdoc />
    public static string ModVersionPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.MVersion);

    /// <inheritdoc />
    public static string InstallerPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.QuiltPath ?? Validate.For.EmptyString(),
            "installers",
            $"quilt-installer-{launcherVersion?.QuiltInstallerVersion}.jar"
        );

    /// <inheritdoc />
    public static string InstallersPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.QuiltPath ?? Validate.For.EmptyString(), "installers");

    /// <inheritdoc />
    public static string VersionManifestPath(LauncherPath? launcherPath) =>
        VFS.Combine(launcherPath?.QuiltPath ?? Validate.For.EmptyString(), "quilt_manifest.json");

    /// <inheritdoc />
    public static string ProfilePath(LauncherPath? launcherPath, LauncherVersion? launcherVersion) =>
        VFS.Combine(
            launcherPath?.QuiltPath ?? Validate.For.EmptyString(),
            $"quilt_profile-{launcherVersion?.MVersion}-{launcherVersion?.QuiltLoaderVersion}.json"
        );

    /// <inheritdoc />
    public static string LoaderJarPath(QuiltUrls? quiltUrls, LauncherVersion? launcherVersion) =>
        quiltUrls?.LoaderJar.Replace("{0}", launcherVersion?.QuiltLoaderVersion) ?? Validate.For.EmptyString();

    /// <inheritdoc />
    public static string LoaderProfilePath(QuiltUrls? quiltUrls, LauncherVersion? launcherVersion) =>
        string.Format(
            quiltUrls?.LoaderProfile ?? Validate.For.EmptyString(),
            launcherVersion?.MVersion,
            launcherVersion?.QuiltLoaderVersion
        );
}
