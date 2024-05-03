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
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Web;

namespace MCL.Core.Minecraft.Web;

public static class AssetIndexDownloader
{
    /// <summary>
    /// Download the game asset index specified by the MVersionDetails.
    /// </summary>
    public static async Task<bool> Download(LauncherPath? launcherPath, MVersionDetails? versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [versionDetails?.AssetIndex?.SHA1, versionDetails?.AssetIndex?.URL, versionDetails?.Assets]
            )
        )
        {
            return false;
        }

        return await Request.DownloadSHA1(
            versionDetails!.AssetIndex!.URL,
            MPathResolver.ClientAssetIndexPath(launcherPath, versionDetails),
            versionDetails!.AssetIndex!.SHA1
        );
    }
}
