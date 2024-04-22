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
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Services;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricInstallerDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricInstaller fabricInstaller
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [launcherVersion?.FabricInstallerVersion, fabricInstaller?.URL, fabricInstaller?.Version]
            )
        )
            return false;

        string fabricInstallerPath = FabricPathResolver.InstallerPath(launcherPath, launcherVersion);
        // Fabric does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(fabricInstallerPath))
        {
            NotificationService.Error("fabric.installer-exists", fabricInstaller?.Version);
            return true;
        }

        return await Request.DownloadSHA1(fabricInstaller.URL, fabricInstallerPath, string.Empty);
    }
}
