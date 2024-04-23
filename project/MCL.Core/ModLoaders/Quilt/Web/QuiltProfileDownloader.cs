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

using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltProfileDownloader
{
    /// <summary>
    /// Download a Quilt profile specified by the QuiltLoaderVersion.
    /// </summary>
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltUrls quiltUrls
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [launcherVersion?.MVersion, launcherVersion?.QuiltLoaderVersion, quiltUrls?.LoaderProfile]
            )
        )
            return false;

        string quiltProfile = await Request.GetJsonAsync<QuiltProfile>(
            QuiltPathResolver.LoaderProfilePath(quiltUrls, launcherVersion),
            QuiltPathResolver.ProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (ObjectValidator<string>.IsNullOrWhiteSpace([quiltProfile]))
            return false;
        return true;
    }
}
