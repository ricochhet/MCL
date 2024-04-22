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
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperVersionHelper
{
    public static async Task<bool> SetVersion(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        PaperServerDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherInstance,
            settings.PaperUrls
        );
        if (!PaperServerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await PaperServerDownloadService.DownloadVersionManifest();
            PaperServerDownloadService.LoadVersionManifest();
        }

        if (ObjectValidator<PaperVersionManifest>.IsNull(PaperServerDownloadService.PaperVersionManifest))
            return false;

        List<string> versions = GetVersionIds(PaperServerDownloadService.PaperVersionManifest);
        string version = launcherVersion.PaperServerVersion;

        if (version == "latest" || ObjectValidator<string>.IsNullOrWhiteSpace([version]))
            version = versions[^1]; // Latest is the last version of the array.

        if (!versions.Contains(version))
            return false;

        settings.LauncherVersion.PaperServerVersion = version;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetVersionIds(PaperVersionManifest paperVersionManifest)
    {
        if (ObjectValidator<List<PaperBuild>>.IsNullOrEmpty(paperVersionManifest?.Builds))
            return [];

        List<string> versions = [];
        foreach (PaperBuild item in paperVersionManifest.Builds)
        {
            versions.Add(item.Build.ToString());
        }

        return versions;
    }

    public static PaperBuild GetVersion(LauncherVersion paperServerVersion, PaperVersionManifest paperVersionManifest)
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace([paperServerVersion?.PaperServerVersion])
            || ObjectValidator<List<PaperBuild>>.IsNullOrEmpty(paperVersionManifest?.Builds)
        )
            return null;

        PaperBuild paperBuild = paperVersionManifest.Builds[^1];
        if (ObjectValidator<string>.IsNullOrWhiteSpace([paperServerVersion?.PaperServerVersion]))
            return paperBuild;

        foreach (PaperBuild item in paperVersionManifest.Builds)
        {
            if (
                ObjectValidator<string>.IsNotNullOrWhiteSpace([paperServerVersion?.PaperServerVersion])
                && item.Build.ToString() == paperServerVersion.PaperServerVersion
            )
                return item;
        }
        return paperBuild;
    }
}
