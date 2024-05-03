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

namespace MCL.Core.ModLoaders.Interfaces.Resolvers;

public interface IModLoaderPathResolver<in T>
{
    /// <summary>
    /// The mod path.
    /// </summary>
    public static abstract string ModPath(LauncherPath launcherPath);

    /// <summary>
    /// The mod version path specified by the MVersion.
    /// </summary>
    public static abstract string ModVersionPath(LauncherPath launcherPath, LauncherVersion launcherVersion);

    /// <summary>
    /// The installer jar path specified by the InstallerVersion.
    /// </summary>
    public static abstract string InstallerPath(LauncherPath? launcherPath, LauncherVersion? launcherVersion);

    /// <summary>
    /// The base installer path for installers.
    /// </summary>
    public static abstract string InstallersPath(LauncherPath? launcherPath);

    /// <summary>
    /// The manifest path.
    /// </summary>
    public static abstract string VersionManifestPath(LauncherPath? launcherPath);

    /// <summary>
    /// The profile path specified by the LoaderVersion.
    /// </summary>
    public static abstract string ProfilePath(LauncherPath? launcherPath, LauncherVersion? launcherVersion);

    /// <summary>
    /// The loader jar path specified by the LoaderVersion.
    /// </summary>
    public static abstract string LoaderJarPath(T? urls, LauncherVersion? launcherVersion);

    /// <summary>
    /// The loader profile path specified by the LoaderVersion.
    /// </summary>
    public static abstract string LoaderProfilePath(T? urls, LauncherVersion? launcherVersion);
}
