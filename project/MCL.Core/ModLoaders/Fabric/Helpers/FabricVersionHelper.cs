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
using MCL.Core.Launcher.Providers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Services;
using MCL.Core.ModLoaders.Interfaces.Helpers;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public class FabricVersionHelper : IModLoaderVersionHelper<FabricVersionManifest, FabricInstaller, FabricLoader>
{
    /// <inheritdoc />
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

        if (ClassValidator.IsNull(downloader.FabricVersionManifest))
            return false;

        List<string> installerVersions = GetInstallerVersionIds(downloader.FabricVersionManifest!);
        string? installerVersion = launcherVersion.FabricInstallerVersion;

        if (installerVersion == "latest" || StringValidator.IsNullOrWhiteSpace([installerVersion]))
            installerVersion = installerVersions.FirstOrDefault();

        if (!installerVersions.Contains(installerVersion ?? StringOperator.Empty()))
            return false;

        if (ClassValidator.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.FabricInstallerVersion = installerVersion!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <inheritdoc />
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

        if (ClassValidator.IsNull(downloader.FabricVersionManifest))
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(downloader.FabricVersionManifest!);
        string? loaderVersion = launcherVersion.FabricLoaderVersion;

        if (loaderVersion == "latest" || StringValidator.IsNullOrWhiteSpace([loaderVersion]))
            loaderVersion = loaderVersions.FirstOrDefault();

        if (!loaderVersions.Contains(loaderVersion ?? StringOperator.Empty()))
            return false;

        if (ClassValidator.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.FabricLoaderVersion = loaderVersion!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <inheritdoc />
    public static List<string> GetInstallerVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (ListValidator.IsNullOrEmpty(fabricVersionManifest?.Installer))
            return [];

        List<string> versions = [];
        foreach (FabricInstaller item in fabricVersionManifest!.Installer!)
            versions.Add(item.Version);

        return versions;
    }

    /// <inheritdoc />
    public static List<string> GetLoaderVersionIds(FabricVersionManifest fabricVersionManifest)
    {
        if (ListValidator.IsNullOrEmpty(fabricVersionManifest?.Loader))
            return [];

        List<string> versions = [];
        foreach (FabricLoader item in fabricVersionManifest!.Loader!)
            versions.Add(item.Version);

        return versions;
    }

    /// <inheritdoc />
    public static FabricInstaller? GetInstallerVersion(
        LauncherVersion? installerVersion,
        FabricVersionManifest? fabricVersionManifest
    )
    {
        if (
            StringValidator.IsNullOrWhiteSpace([installerVersion?.FabricInstallerVersion])
            || ListValidator.IsNullOrEmpty(fabricVersionManifest?.Installer)
        )
        {
            return null;
        }

        FabricInstaller? fabricInstaller = fabricVersionManifest!.Installer!.FirstOrDefault();
        foreach (FabricInstaller item in fabricVersionManifest!.Installer!)
        {
            if (item.Version == installerVersion!.FabricInstallerVersion)
                return item;
        }
        return fabricInstaller;
    }

    /// <inheritdoc />
    public static FabricLoader? GetLoaderVersion(
        LauncherVersion? loaderVersion,
        FabricVersionManifest? fabricVersionManifest
    )
    {
        if (
            StringValidator.IsNullOrWhiteSpace([loaderVersion?.FabricLoaderVersion])
            || ListValidator.IsNullOrEmpty(fabricVersionManifest?.Loader)
        )
        {
            return null;
        }

        FabricLoader? fabricLoader = fabricVersionManifest!.Loader!.FirstOrDefault();
        foreach (FabricLoader item in fabricVersionManifest!.Loader!)
        {
            if (item.Version == loaderVersion!.FabricLoaderVersion)
                return item;
        }
        return fabricLoader;
    }
}
