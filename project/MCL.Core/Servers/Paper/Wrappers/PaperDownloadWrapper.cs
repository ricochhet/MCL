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
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Core.Servers.Paper.Wrappers;

public static class PaperDownloadWrapper
{
    public static async Task<bool> Download(Settings? settings, LauncherVersion launcherVersion, bool update)
    {
        if (ClassValidator.IsNull(settings))
            return false;
        if (StringValidator.IsNullOrWhiteSpace([launcherVersion.MVersion, launcherVersion.PaperServerVersion]))
            return false;
        if (!await VersionHelper.SetVersion(settings, launcherVersion, update))
            return false;
        if (!await PaperVersionHelper.SetVersion(settings, launcherVersion, update))
            return false;

        PaperServerDownloadService downloader =
            new(settings!?.LauncherPath, settings!?.LauncherVersion, settings!?.LauncherInstance, settings!?.PaperUrls);

        return await downloader.Download(loadLocalVersionManifest: true);
    }
}
