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
using MCL.Core.ModLoaders.Interfaces.Web;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MiniCommon.IO;
using MiniCommon.Providers;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.ModLoaders.Quilt.Web;

public class QuiltInstallerDownloader : IModLoaderInstallerDownloader<QuiltInstaller>
{
    /// <inheritdoc />
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        QuiltInstaller? quiltInstaller
    )
    {
        if (
            StringValidator.IsNullOrWhiteSpace(
                [launcherVersion?.QuiltInstallerVersion, quiltInstaller?.URL, quiltInstaller?.Version]
            )
        )
        {
            return false;
        }

        string quiltInstallerPath = QuiltPathResolver.InstallerPath(launcherPath, launcherVersion);
        // Quilt does not provide a file hash through the current method. We do simple check of the version instead.
        if (VFS.Exists(quiltInstallerPath))
        {
            NotificationProvider.Info("quilt.installer-exists", quiltInstaller!.Version);
            return true;
        }

        return await Request.DownloadSHA1(quiltInstaller!.URL, quiltInstallerPath, string.Empty);
    }
}
