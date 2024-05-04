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
using MiniCommon.IO;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.Minecraft.Web;

public static class ResourceDownloader
{
    /// <summary>
    /// Download the game resources specified by the MAssetsData.
    /// </summary>
    public static async Task<bool> Download(LauncherPath? launcherPath, MUrls? mUrls, MAssetsData? assets)
    {
        if (
            StringValidator.IsNullOrWhiteSpace([mUrls?.MinecraftResources])
            || DictionaryValidator.IsNullOrEmpty(assets?.Objects)
        )
        {
            return false;
        }

        string objectsPath = VFS.Combine(MPathResolver.AssetsPath(launcherPath), "objects");
        foreach ((_, MAsset asset) in assets!.Objects!)
        {
            if (StringValidator.IsNullOrWhiteSpace([asset?.Hash]))
                return false;

            string request = $"{mUrls!.MinecraftResources}/{asset!.Hash[..2]}/{asset!.Hash}";
            string filepath = VFS.Combine(objectsPath, asset!.Hash[..2], asset!.Hash);
            if (!await Request.DownloadSHA1(request, filepath, asset!.Hash))
                return false;
        }

        return true;
    }
}
