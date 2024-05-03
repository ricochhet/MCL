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
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Interfaces.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public class QuiltVersionHelper : IModLoaderVersionHelper<QuiltVersionManifest, QuiltInstaller, QuiltLoader>
{
    /// <inheritdoc />
    public static async Task<bool> SetInstallerVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        QuiltInstallerDownloadService downloader =
            new(settings?.LauncherPath, settings?.LauncherVersion, settings?.QuiltUrls);
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (ObjectValidator<QuiltVersionManifest>.IsNull(downloader.QuiltVersionManifest))
            return false;

        List<string> installerVersions = GetInstallerVersionIds(downloader.QuiltVersionManifest!);
        string? installerVersion = launcherVersion.QuiltInstallerVersion;

        if (installerVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion]))
            installerVersion = installerVersions.FirstOrDefault();

        if (!installerVersions.Contains(installerVersion ?? ValidationShims.StringEmpty()))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.QuiltInstallerVersion = installerVersion!;
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
        QuiltLoaderDownloadService downloader =
            new(settings?.LauncherPath, settings?.LauncherVersion, settings?.LauncherInstance, settings?.QuiltUrls);
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (ObjectValidator<QuiltVersionManifest>.IsNull(downloader.QuiltVersionManifest))
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(downloader.QuiltVersionManifest!);
        string? loaderVersion = launcherVersion.QuiltLoaderVersion;

        if (loaderVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion]))
            loaderVersion = loaderVersions.FirstOrDefault();

        if (!loaderVersions.Contains(loaderVersion ?? ValidationShims.StringEmpty()))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.QuiltLoaderVersion = loaderVersion!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <inheritdoc />
    public static List<string> GetInstallerVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (ObjectValidator<List<QuiltInstaller>>.IsNullOrEmpty(quiltVersionManifest?.Installer))
            return [];

        List<string> versions = [];
        foreach (QuiltInstaller item in quiltVersionManifest!.Installer!)
            versions.Add(item.Version);

        return versions;
    }

    /// <inheritdoc />
    public static List<string> GetLoaderVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (ObjectValidator<List<QuiltLoader>>.IsNullOrEmpty(quiltVersionManifest?.Loader))
            return [];

        List<string> versions = [];
        foreach (QuiltLoader item in quiltVersionManifest!.Loader!)
            versions.Add(item.Version);

        return versions;
    }

    /// <inheritdoc />
    public static QuiltInstaller? GetInstallerVersion(
        LauncherVersion? installerVersion,
        QuiltVersionManifest? quiltVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.QuiltInstallerVersion])
            || ObjectValidator<List<QuiltInstaller>>.IsNullOrEmpty(quiltVersionManifest?.Installer)
        )
        {
            return null;
        }

        QuiltInstaller? quiltInstaller = quiltVersionManifest!.Installer!.FirstOrDefault();
        foreach (QuiltInstaller item in quiltVersionManifest!.Installer!)
        {
            if (item.Version == installerVersion!.QuiltInstallerVersion)
                return item;
        }
        return quiltInstaller;
    }

    /// <inheritdoc />
    public static QuiltLoader? GetLoaderVersion(
        LauncherVersion? loaderVersion,
        QuiltVersionManifest? quiltVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.QuiltLoaderVersion])
            || ObjectValidator<List<QuiltLoader>>.IsNullOrEmpty(quiltVersionManifest?.Loader)
        )
        {
            return null;
        }

        QuiltLoader? quiltLoader = quiltVersionManifest!.Loader!.FirstOrDefault();
        foreach (QuiltLoader item in quiltVersionManifest!.Loader!)
        {
            if (item.Version == loaderVersion!.QuiltLoaderVersion)
                return item;
        }
        return quiltLoader;
    }
}
