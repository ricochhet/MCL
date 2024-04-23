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

using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricLoaderDownloader
{
#pragma warning disable IDE0079
#pragma warning disable S3776
    /// <summary>
    /// Download a Fabric loader specified by the FabricProfile object.
    /// </summary>
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        FabricProfile fabricProfile,
        FabricUrls fabricUrls
    )
#pragma warning restore
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [launcherVersion?.FabricLoaderVersion, fabricUrls?.ApiLoaderName, fabricUrls?.ApiIntermediaryName]
            ) || ObjectValidator<List<FabricLibrary>>.IsNullOrEmpty(fabricProfile?.Libraries)
        )
            return false;

        LauncherLoader loader = new() { Version = launcherVersion.FabricLoaderVersion };

        foreach (FabricLibrary library in fabricProfile.Libraries)
        {
            if (ObjectValidator<string>.IsNullOrWhiteSpace([library?.Name, library?.URL]))
                return false;

            string request;
            string hash;
            if (library.Name.Contains(fabricUrls.ApiLoaderName))
            {
                request = FabricPathResolver.LoaderJarPath(fabricUrls, launcherVersion);
                hash = string.Empty;
            }
            else if (library.Name.Contains(fabricUrls.ApiIntermediaryName))
            {
                request = FabricLibrary.ParseURL(library.Name, library.URL);
                hash = string.Empty;
            }
            else
            {
                if (ObjectValidator<string>.IsNullOrWhiteSpace([library.SHA1]))
                    return false;
                request = FabricLibrary.ParseURL(library.Name, library.URL);
                hash = library.SHA1;
            }

            string filepath = VFS.Combine(
                MPathResolver.LibraryPath(launcherPath),
                FabricLibrary.ParsePath(library.Name)
            );
            loader.Libraries.Add(filepath);

            if (!await Request.DownloadSHA1(request, filepath, hash))
                return false;
        }

        foreach (LauncherLoader existingLoader in launcherInstance.FabricLoaders)
        {
            if (existingLoader.Version == loader.Version)
                launcherInstance.FabricLoaders.Remove(existingLoader);
        }

        launcherInstance.FabricLoaders.Add(loader);
        SettingsService.Load().Save(launcherInstance);
        return true;
    }
}
