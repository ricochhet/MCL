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
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Interfaces.Web;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.ModLoaders.Fabric.Web;

public class FabricVersionManifestDownloader : IModLoaderVersionManifestDownloader<FabricUrls>
{
    /// <inheritdoc />
    public static async Task<bool> Download(LauncherPath? launcherPath, FabricUrls? fabricUrls)
    {
        if (Validate.For.IsNullOrWhiteSpace([fabricUrls?.VersionManifest]))
            return false;

        string filepath = FabricPathResolver.VersionManifestPath(launcherPath);
        string? fabricVersionManifest = await Request.GetJsonAsync<FabricVersionManifest>(
            fabricUrls!.VersionManifest!,
            filepath,
            Encoding.UTF8,
            FabricVersionManifestContext.Default
        );
        return Validate.For.IsNotNullOrWhiteSpace([fabricVersionManifest]);
    }
}
