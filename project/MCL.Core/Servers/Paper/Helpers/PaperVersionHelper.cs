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
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperVersionHelper
{
    /// <summary>
    /// Get the PaperVersionManifest and set the version of PaperServerVersion in Settings.
    /// </summary>
    public static async Task<bool> SetVersion(
        Settings? settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        PaperServerDownloadService.Init(
            settings?.LauncherPath,
            settings?.LauncherVersion,
            settings?.LauncherInstance,
            settings?.PaperUrls
        );
        if (!PaperServerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await PaperServerDownloadService.DownloadVersionManifest();
            PaperServerDownloadService.LoadVersionManifest();
        }

        if (ObjectValidator<PaperVersionManifest>.IsNull(PaperServerDownloadService.PaperVersionManifest))
            return false;

        List<string> versions = GetVersionIds(
            PaperServerDownloadService.PaperVersionManifest ?? ValidationShims.ClassEmpty<PaperVersionManifest>()
        );
        string? version = launcherVersion.PaperServerVersion;

        if (version == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([version]))
            version = versions.LastOrDefault(); // Latest is the last version of the array.

        if (!versions.Contains(version ?? ValidationShims.StringEmpty()))
            return false;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return false;
#pragma warning disable CS8602
        settings.LauncherVersion.PaperServerVersion = version ?? ValidationShims.StringEmpty();
#pragma warning restore CS8602
        SettingsService.Save(settings);
        return true;
    }

    /// <summary>
    /// Get a list of version identifiers.
    /// </summary>
    public static List<string> GetVersionIds(PaperVersionManifest paperVersionManifest)
    {
        if (ObjectValidator<List<PaperBuild>>.IsNullOrEmpty(paperVersionManifest?.Builds))
            return [];

        List<string> versions = [];
        foreach (PaperBuild item in paperVersionManifest?.Builds ?? ValidationShims.ListEmpty<PaperBuild>())
        {
            versions.Add(item.Build.ToString());
        }

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
            ObjectValidator<string>.IsNullOrWhiteSpace([paperServerVersion?.PaperServerVersion])
            || ObjectValidator<List<PaperBuild>>.IsNullOrEmpty(paperVersionManifest?.Builds)
        )
            return null;

        PaperBuild? paperBuild = paperVersionManifest?.Builds?.LastOrDefault();
        if (ObjectValidator<string>.IsNullOrWhiteSpace([paperServerVersion?.PaperServerVersion]))
            return paperBuild;

        foreach (PaperBuild item in paperVersionManifest?.Builds ?? ValidationShims.ListEmpty<PaperBuild>())
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([paperServerVersion?.PaperServerVersion])
                && item.Build.ToString() == paperServerVersion?.PaperServerVersion
            )
                return item;
        }
        return paperBuild;
    }
}
