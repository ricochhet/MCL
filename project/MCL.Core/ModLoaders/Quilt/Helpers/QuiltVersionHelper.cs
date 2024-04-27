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
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public static class QuiltVersionHelper
{
    /// <summary>
    /// Get the Fabric Quilt and set the version of QuiltInstallerVersion in Settings.
    /// </summary>
    public static async Task<bool> SetInstallerVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        QuiltInstallerDownloadService.Init(settings?.LauncherPath, settings?.LauncherVersion, settings?.QuiltUrls);
        if (!QuiltInstallerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await QuiltInstallerDownloadService.DownloadVersionManifest();
            QuiltInstallerDownloadService.LoadVersionManifest();
        }

        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltInstallerDownloadService.QuiltVersionManifest))
            return false;

        List<string> installerVersions = GetInstallerVersionIds(
            QuiltInstallerDownloadService.QuiltVersionManifest ?? ValidationShims.ClassEmpty<QuiltVersionManifest>()
        );
        string installerVersion = launcherVersion.QuiltInstallerVersion;

        if (installerVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion]))
            installerVersion = installerVersions[0];

        if (!installerVersions.Contains(installerVersion))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
#pragma warning disable CS8602
        settings.LauncherVersion.QuiltInstallerVersion = installerVersion;
#pragma warning restore CS8602
        SettingsService.Save(settings);
        return true;
    }

    /// <summary>
    /// Get the Quilt manifest and set the version of QuiltLoaderVersion in Settings.
    /// </summary>
    public static async Task<bool> SetLoaderVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        QuiltLoaderDownloadService.Init(
            settings?.LauncherPath,
            settings?.LauncherVersion,
            settings?.LauncherInstance,
            settings?.QuiltUrls
        );
        if (!QuiltLoaderDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await QuiltLoaderDownloadService.DownloadVersionManifest();
            QuiltLoaderDownloadService.LoadVersionManifest();
        }

        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltLoaderDownloadService.QuiltVersionManifest))
            return false;

        List<string> loaderVersions = GetLoaderVersionIds(
            QuiltLoaderDownloadService.QuiltVersionManifest ?? ValidationShims.ClassEmpty<QuiltVersionManifest>()
        );
        string loaderVersion = launcherVersion.QuiltLoaderVersion;

        if (loaderVersion == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion]))
            loaderVersion = loaderVersions[0];

        if (!loaderVersions.Contains(loaderVersion))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
#pragma warning disable CS8602
        settings.LauncherVersion.QuiltLoaderVersion = loaderVersion;
#pragma warning restore CS8602
        SettingsService.Save(settings);
        return true;
    }

    /// <summary>
    /// Get a list of Quilt installer version identifiers.
    /// </summary>
    public static List<string> GetInstallerVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (ObjectValidator<List<QuiltInstaller>>.IsNullOrEmpty(quiltVersionManifest?.Installer))
            return [];

        List<string> versions = [];
        foreach (QuiltInstaller item in quiltVersionManifest?.Installer ?? ValidationShims.ListEmpty<QuiltInstaller>())
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    /// <summary>
    /// Get a list of Quilt loader version identifiers.
    /// </summary>
    public static List<string> GetLoaderVersionIds(QuiltVersionManifest quiltVersionManifest)
    {
        if (ObjectValidator<List<QuiltLoader>>.IsNullOrEmpty(quiltVersionManifest?.Loader))
            return [];

        List<string> versions = [];
        foreach (QuiltLoader item in quiltVersionManifest?.Loader ?? ValidationShims.ListEmpty<QuiltLoader>())
        {
            versions.Add(item.Version);
        }

        return versions;
    }

    /// <summary>
    /// Get a QuiltInstaller object from the QuiltVersionManifest.
    /// </summary>
    public static QuiltInstaller? GetInstallerVersion(
        LauncherVersion? installerVersion,
        QuiltVersionManifest? quiltVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.QuiltInstallerVersion])
            || ObjectValidator<List<QuiltInstaller>>.IsNullOrEmpty(quiltVersionManifest?.Installer)
        )
            return null;

        QuiltInstaller? quiltInstaller = quiltVersionManifest?.Installer?.FirstOrDefault();
        if (ObjectValidator<string>.IsNullOrWhiteSpace([installerVersion?.QuiltInstallerVersion]))
            return quiltInstaller;

        foreach (QuiltInstaller item in quiltVersionManifest?.Installer ?? ValidationShims.ListEmpty<QuiltInstaller>())
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([installerVersion?.QuiltInstallerVersion])
                && item.Version == installerVersion?.QuiltInstallerVersion
            )
                return item;
        }
        return quiltInstaller;
    }

    /// <summary>
    /// Get a QuiltLoader object from the QuiltVersionManifest.
    /// </summary>
    public static QuiltLoader? GetLoaderVersion(
        LauncherVersion? loaderVersion,
        QuiltVersionManifest? quiltVersionManifest
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.QuiltLoaderVersion])
            || ObjectValidator<List<QuiltLoader>>.IsNullOrEmpty(quiltVersionManifest?.Loader)
        )
            return null;

        QuiltLoader? quiltLoader = quiltVersionManifest?.Loader?.FirstOrDefault();
        if (ObjectValidator<string>.IsNullOrWhiteSpace([loaderVersion?.QuiltLoaderVersion]))
            return quiltLoader;

        foreach (QuiltLoader item in quiltVersionManifest?.Loader ?? ValidationShims.ListEmpty<QuiltLoader>())
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([loaderVersion?.QuiltLoaderVersion])
                && item.Version == loaderVersion?.QuiltLoaderVersion
            )
                return item;
        }
        return quiltLoader;
    }
}
