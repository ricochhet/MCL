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
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Providers;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Services;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Minecraft.Helpers;

public static class VersionHelper
{
    /// <summary>
    /// Check if the launcher version based on the ClientType exists.
    /// </summary>
    public static bool VersionExists(Settings settings)
    {
        if (Validate.For.IsNull(settings) || Validate.For.IsNull(settings.LauncherInstance))
        {
            return false;
        }

        return settings.LauncherSettings!.ClientType switch
        {
            ClientType.VANILLA => Validate.For.IsNotNullOrEmpty(settings.LauncherInstance!.Versions),
            ClientType.FABRIC => Validate.For.IsNotNullOrEmpty(settings.LauncherInstance!.FabricLoaders),
            ClientType.QUILT => Validate.For.IsNotNullOrEmpty(settings.LauncherInstance!.QuiltLoaders),
            _ => false,
        };
    }

    /// <summary>
    /// Get the MVersionManifest and set the version of MVersion in Settings.
    /// </summary>
    public static async Task<bool> SetVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        MDownloadService downloader =
            new(
                settings?.LauncherPath,
                settings?.LauncherVersion,
                settings?.LauncherSettings,
                settings?.LauncherInstance,
                settings?.MUrls
            );
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (Validate.For.IsNull(downloader.VersionManifest))
            return false;

        List<string> versions = GetVersionIds(downloader.VersionManifest);
        string? version = launcherVersion.MVersion;

        if (version == "latest" || Validate.For.IsNullOrWhiteSpace([version]))
            version = versions.FirstOrDefault();

        if (!versions.Contains(version ?? Validate.For.EmptyString()))
            return false;

        if (Validate.For.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.MVersion = version!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <summary>
    /// Get a list of version identifiers.
    /// </summary>
    public static List<string> GetVersionIds(MVersionManifest? versionManifest)
    {
        if (Validate.For.IsNullOrEmpty(versionManifest?.Versions))
            return [];

        List<string> versions = [];
        foreach (MVersion item in versionManifest!.Versions!)
            versions.Add(item.ID);

        return versions;
    }

    /// <summary>
    /// Get a MVersion object from the MVersionManifest.
    /// </summary>
    public static MVersion? GetVersion(LauncherVersion? launcherVersion, MVersionManifest? versionManifest)
    {
        if (Validate.For.IsNullOrEmpty(versionManifest?.Versions))
            return null;

        foreach (MVersion item in versionManifest!.Versions!)
        {
            if (
                Validate.For.IsNullOrWhiteSpace([launcherVersion?.MVersion])
                && item.ID == versionManifest!.Latest?.Release
            )
            {
                return item;
            }

            if (Validate.For.IsNotNullOrWhiteSpace([launcherVersion?.MVersion]) && item.ID == launcherVersion?.MVersion)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Get a MVersionDetails object from the MVersion.
    /// </summary>
    public static MVersionDetails? GetVersionDetails(LauncherPath? launcherPath, LauncherVersion? launcherVersion)
    {
        if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.MVersion]))
            return null;

        MVersionManifest? versionManifest = Json.Load<MVersionManifest>(
            MPathResolver.VersionManifestPath(launcherPath),
            MVersionManifestContext.Default
        );

        if (Validate.For.IsNullOrEmpty(versionManifest?.Versions))
            return null;

        MVersion? version = GetVersion(launcherVersion, versionManifest);
        if (Validate.For.IsNull(version))
            return null;

        MVersionDetails? versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(launcherPath, version),
            MVersionDetailsContext.Default
        );
        return Validate.For.IsNotNull(versionDetails) ? versionDetails : null;
    }
}
