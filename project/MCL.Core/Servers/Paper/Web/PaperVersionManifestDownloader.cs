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
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.Servers.Paper.Web;

public static class PaperVersionManifestDownloader
{
    /// <summary>
    /// Download the Paper server version manifest.
    /// </summary>
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        PaperUrls? paperUrls
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.MVersion, paperUrls?.VersionManifest]))
            return false;

        string filepath = PaperPathResolver.VersionManifestPath(launcherPath, launcherVersion);
        string? paperIndex = await Request.GetJsonAsync<PaperVersionManifest>(
            string.Format(paperUrls!.VersionManifest, "paper", launcherVersion!.MVersion),
            filepath,
            Encoding.UTF8,
            PaperVersionManifestContext.Default
        );
        return Validate.For.IsNotNullOrWhiteSpace([paperIndex]);
    }
}
