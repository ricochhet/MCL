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

using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Java.Wrappers;

public static class MDownloadWrapper
{
    public static async Task<bool> Download(Settings? settings, LauncherVersion launcherVersion, bool update)
    {
        if (ObjectValidator<Settings>.IsNull(settings))
            return false;
        if (ObjectValidator<string>.IsNullOrWhiteSpace([launcherVersion.MVersion]))
            return false;
        if (!await VersionHelper.SetVersion(settings, launcherVersion, update))
            return false;

        MDownloadService.Init(
            settings!?.LauncherPath,
            settings!?.LauncherVersion,
            settings!?.LauncherSettings,
            settings!?.LauncherInstance,
            settings!?.MUrls
        );
        if (!await MDownloadService.Download(loadLocalVersionManifest: true))
            return false;

        return true;
    }
}
