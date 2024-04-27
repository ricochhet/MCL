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
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Web;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricVersionManifestDownloader
{
    /// <summary>
    /// Download the Fabric version manifest.
    /// </summary>
    public static async Task<bool> Download(LauncherPath? launcherPath, FabricUrls? fabricUrls)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([fabricUrls?.VersionManifest]))
            return false;

        string filepath = FabricPathResolver.VersionManifestPath(launcherPath);
        string? fabricVersionManifest = await Request.GetJsonAsync<FabricVersionManifest>(
            fabricUrls?.VersionManifest ?? ValidationShims.StringEmpty(),
            filepath,
            Encoding.UTF8
        );
        if (ObjectValidator<string>.IsNullOrWhiteSpace([fabricVersionManifest]))
            return false;
        return true;
    }
}
