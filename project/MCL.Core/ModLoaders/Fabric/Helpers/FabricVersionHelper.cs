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
using System.Linq;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricVersionHelper
{
    /// <summary>
    /// Get the Fabric manifest and set the version of FabricInstallerVersion in Settings.
    /// </summary>
    public static async Task<bool> SetInstallerVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        FabricInstallerDownloadService downloader =
            new(settings?.LauncherPath, settings?.LauncherVersion, settings?.FabricUrls);
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (ObjectValidator<FabricVersionManifest>.IsNull(downloader.FabricVersionManifest))
            return false;

        List<string> installerVersions = GetInstallerVersionIds(downloader.FabricVersionManifest!);
        string? installerVersion = launcherVersion.FabricInstallerVersion;

        if (installerVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion]))
            installerVersion = installerVersions.FirstOrDefault();

        if (!installerVersions.Contains(installerVersion ?? ValidationShims.StringEmpty()))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.FabricInstallerVersion = installerVersion!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <summary>
    /// Get the Fabric manifest and set the version of FabricLoaderVersion in Settings.
    /// </summary>
    public static async Task<bool> SetLoaderVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        FabricLoaderDownloadService downloader =
            new(settings?.LauncherPath, settings?.LauncherVersion, settings?.LauncherInstance, settings?.FabricUrls);
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (ObjectValidator<FabricVersionManifest>.IsNull(downloader.FabricVersionManifest))
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(downloader.FabricVersionManifest!);
        string? loaderVersion = launcherVersion.FabricLoaderVersion;

        if (loaderVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion]))
            loaderVersion = loaderVersions.FirstOrDefault();

        if (!loaderVersions.Contains(loaderVersion ?? ValidationShims.StringEmpty()))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.FabricLoaderVersion = loaderVersion!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <summary>
    /// Get a list of Fabric installer version identifiers.
    /// </summary>
    public static List<string> GetInstallerVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (ObjectValidator<List<FabricInstaller>>.IsNullOrEmpty(fabricVersionManifest?.Installer))
            return [];

        List<string> versions = [];
        foreach (FabricInstaller item in fabricVersionManifest!.Installer!)
            versions.Add(item.Version);

        return versions;
    }

    /// <summary>
    /// Get a list of Fabric loader version identifiers.
    /// </summary>
    public static List<string> GetLoaderVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (ObjectValidator<List<FabricLoader>>.IsNullOrEmpty(fabricVersionManifest?.Loader))
            return [];

        List<string> versions = [];
        foreach (FabricLoader item in fabricVersionManifest!.Loader!)
            versions.Add(item.Version);

        return versions;
    }

    /// <summary>
    /// Get a FabricInstaller object from the FabricVersionManifest.
    /// </summary>
    public static FabricInstaller? GetInstallerVersion(
        LauncherVersion? installerVersion,
        FabricVersionManifest? fabricVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.FabricInstallerVersion])
            || ObjectValidator<List<FabricInstaller>>.IsNullOrEmpty(fabricVersionManifest?.Installer)
        )
            return null;

        FabricInstaller? fabricInstaller = fabricVersionManifest!.Installer!.FirstOrDefault();
        foreach (FabricInstaller item in fabricVersionManifest!.Installer!)
        {
            if (item.Version == installerVersion!.FabricInstallerVersion)
                return item;
        }
        return fabricInstaller;
    }

    /// <summary>
    /// Get a FabricLoader object from the FabricVersionManifest.
    /// </summary>
    public static FabricLoader? GetLoaderVersion(
        LauncherVersion? loaderVersion,
        FabricVersionManifest? fabricVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.FabricLoaderVersion])
            || ObjectValidator<List<FabricLoader>>.IsNullOrEmpty(fabricVersionManifest?.Loader)
        )
            return null;

        FabricLoader? fabricLoader = fabricVersionManifest!.Loader!.FirstOrDefault();
        foreach (FabricLoader item in fabricVersionManifest!.Loader!)
        {
            if (item.Version == loaderVersion!.FabricLoaderVersion)
                return item;
        }
        return fabricLoader;
    }
}
