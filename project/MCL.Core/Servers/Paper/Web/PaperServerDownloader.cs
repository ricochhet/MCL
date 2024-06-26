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
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.Servers.Paper.Web;

public static class PaperServerDownloader
{
    /// <summary>
    /// Download a Paper server specified by the PaperBuild object.
    /// </summary>
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        PaperBuild? paperBuild,
        PaperUrls? paperUrls
    )
    {
        if (
            Validate.For.IsNullOrWhiteSpace(
                [
                    paperUrls?.PaperJar,
                    paperBuild?.Build.ToString(),
                    paperBuild?.Downloads?.Application?.Name,
                    paperBuild?.Downloads?.Application?.SHA256,
                    launcherVersion?.MVersion
                ]
            )
        )
        {
            return false;
        }

        PaperServerProperties.NewEula(launcherPath, launcherVersion);
        PaperServerProperties.NewProperties(launcherPath, launcherVersion);

        string filepath = PaperPathResolver.JarPath(launcherPath, launcherVersion);

        return await Request.DownloadSHA256(
            string.Format(
                paperUrls!.PaperJar,
                "paper",
                launcherVersion!.MVersion,
                paperBuild!.Build!.ToString(),
                paperBuild!.Downloads!.Application!.Name
            ),
            filepath,
            paperBuild!.Downloads!.Application!.SHA256!
        );
    }
}
