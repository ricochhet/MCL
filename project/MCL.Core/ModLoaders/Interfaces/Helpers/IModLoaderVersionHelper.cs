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
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;

namespace MCL.Core.ModLoaders.Interfaces.Helpers;

#pragma warning disable IDE0079
#pragma warning disable S2436
public interface IModLoaderVersionHelper<in T1, T2, T3>
#pragma warning restore IDE0079, S2436
{
    /// <summary>
    /// Get the manifest and set the version of InstallerVersion in Settings.
    /// </summary>
    public static abstract Task<bool> SetInstallerVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    );

    /// <summary>
    /// Get the manifest and set the version of LoaderVersion in Settings.
    /// </summary>
    public static abstract Task<bool> SetLoaderVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    );

    /// <summary>
    /// Get a list of installer version identifiers.
    /// </summary>
    public static abstract List<string> GetInstallerVersionIds(T1 versionManifest);

    /// <summary>
    /// Get a list of loader version identifiers.
    /// </summary>
    public static abstract List<string> GetLoaderVersionIds(T1 versionManifest);

    /// <summary>
    /// Get an Installer object from the VersionManifest.
    /// </summary>
    public static abstract T2? GetInstallerVersion(LauncherVersion? installerVersion, T1? versionManifest);

    /// <summary>
    /// Get a Loader object from the VersionManifest.
    /// </summary>
    public static abstract T3? GetLoaderVersion(LauncherVersion? loaderVersion, T1? versionManifest);
}
