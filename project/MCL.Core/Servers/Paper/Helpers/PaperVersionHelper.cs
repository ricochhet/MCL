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
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperVersionHelper
{
    /// <summary>
    /// Check if the paper server version exists.
    /// </summary>
    public static bool VersionExists(Settings settings)
    {
        if (
            ClassValidator.IsNull(settings)
            || ClassValidator.IsNull(settings.LauncherInstance)
            || ClassValidator.IsNull(settings.LauncherVersion)
        )
        {
            return false;
        }

        return settings.LauncherInstance!.PaperServerVersions.Contains(settings.LauncherVersion!.PaperServerVersion);
    }

    /// <summary>
    /// Get the PaperVersionManifest and set the version of PaperServerVersion in Settings.
    /// </summary>
    public static async Task<bool> SetVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        PaperServerDownloadService downloader =
            new(settings?.LauncherPath, settings?.LauncherVersion, settings?.LauncherInstance, settings?.PaperUrls);
        if (!downloader.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await downloader.DownloadVersionManifest();
            downloader.LoadVersionManifest();
        }

        if (ClassValidator.IsNull(downloader.PaperVersionManifest))
            return false;

        List<string> versions = GetVersionIds(downloader.PaperVersionManifest!);
        string? version = launcherVersion.PaperServerVersion;

        if (version == "latest" || StringValidator.IsNullOrWhiteSpace([version]))
            version = versions.LastOrDefault(); // Latest is the last version of the array.

        if (!versions.Contains(version ?? StringOperator.Empty()))
            return false;

        if (ClassValidator.IsNull(settings?.LauncherVersion))
            return false;
        settings!.LauncherVersion!.PaperServerVersion = version!;
        SettingsProvider.Save(settings);
        return true;
    }

    /// <summary>
    /// Get a list of version identifiers.
    /// </summary>
    public static List<string> GetVersionIds(PaperVersionManifest paperVersionManifest)
    {
        if (ListValidator.IsNullOrEmpty(paperVersionManifest?.Builds))
            return [];

        List<string> versions = [];
        foreach (PaperBuild item in paperVersionManifest!.Builds!)
            versions.Add(item.Build.ToString());

        return versions;
    }

    /// <summary>
    /// Get a PaperBuild object from the PaperVersionManifest.
    /// </summary>
    public static PaperBuild? GetVersion(
        LauncherVersion? paperServerVersion,
        PaperVersionManifest? paperVersionManifest
    )
    {
        if (
            StringValidator.IsNullOrWhiteSpace([paperServerVersion?.PaperServerVersion])
            || ListValidator.IsNullOrEmpty(paperVersionManifest?.Builds)
        )
        {
            return null;
        }

        PaperBuild? paperBuild = paperVersionManifest!.Builds!.LastOrDefault();
        foreach (PaperBuild item in paperVersionManifest!.Builds!)
        {
            if (item.Build.ToString() == paperServerVersion!.PaperServerVersion)
                return item;
        }
        return paperBuild;
    }
}
