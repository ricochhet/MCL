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
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Services;

namespace MCL.Core.ModLoaders.Fabric.Wrappers;

public static class FabricLoaderDownloadWrapper
{
    public static async Task<bool> Download(Settings? settings, LauncherVersion launcherVersion, bool update)
    {
        if (ObjectValidator<Settings>.IsNull(settings))
            return false;
        if (ObjectValidator<string>.IsNullOrWhiteSpace([launcherVersion.MVersion, launcherVersion.FabricLoaderVersion]))
            return false;
        if (!await VersionHelper.SetVersion(settings, launcherVersion, update))
            return false;
        if (!await FabricVersionHelper.SetLoaderVersion(settings, launcherVersion, update))
            return false;

        FabricLoaderDownloadService.Init(
            settings!?.LauncherPath,
            settings!?.LauncherVersion,
            settings!?.LauncherInstance,
            settings!?.FabricUrls
        );
        if (!await FabricLoaderDownloadService.Download(loadLocalVersionManifest: true))
            return false;

        return true;
    }
}
