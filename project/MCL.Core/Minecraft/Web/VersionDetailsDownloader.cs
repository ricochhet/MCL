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
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Web;

namespace MCL.Core.Minecraft.Web;

public static class VersionDetailsDownloader
{
    /// <summary>
    /// Download the game version details specified by the MVersion.
    /// </summary>
    public static async Task<bool> Download(LauncherPath? launcherPath, MVersion? version)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([version?.URL, version?.ID]))
            return false;

        string filepath = MPathResolver.VersionDetailsPath(launcherPath, version);
        string? versionDetails = await Request.GetJsonAsync<MVersionDetails>(
            version!.URL,
            filepath,
            Encoding.UTF8,
            MVersionDetailsContext.Default
        );
        return ObjectValidator<string>.IsNotNullOrWhiteSpace([versionDetails]);
    }
}
