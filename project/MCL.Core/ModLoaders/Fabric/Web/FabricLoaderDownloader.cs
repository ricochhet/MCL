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
using MCL.Core.Launcher.Providers;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Interfaces.Web;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;
using MiniCommon.Web;

namespace MCL.Core.ModLoaders.Fabric.Web;

public class FabricLoaderDownloader : IModLoaderLoaderDownloader<FabricProfile, FabricUrls>
{
#pragma warning disable S3776
    /// <inheritdoc />
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        FabricProfile? fabricProfile,
        FabricUrls? fabricUrls
    )
#pragma warning restore S3776
    {
        if (
            Validate.For.IsNullOrWhiteSpace(
                [launcherVersion?.FabricLoaderVersion, fabricUrls?.ApiLoaderName, fabricUrls?.ApiIntermediaryName]
            ) || Validate.For.IsNullOrEmpty(fabricProfile?.Libraries)
        )
        {
            return false;
        }

        LauncherLoader loader = new() { Version = launcherVersion!.FabricLoaderVersion };

        foreach (FabricLibrary library in fabricProfile!.Libraries!)
        {
            if (Validate.For.IsNullOrWhiteSpace([library?.Name, library?.URL]))
                return false;

            string request;
            string hash;
            if (library!.Name.Contains(fabricUrls!.ApiLoaderName))
            {
                request = FabricPathResolver.LoaderJarPath(fabricUrls, launcherVersion);
                hash = string.Empty;
            }
            else if (library!.Name.Contains(fabricUrls!.ApiIntermediaryName))
            {
                request = FabricLibrary.ParseURL(library.Name, library.URL);
                hash = string.Empty;
            }
            else
            {
                if (Validate.For.IsNullOrWhiteSpace([library!.SHA1]))
                    return false;
                request = FabricLibrary.ParseURL(library!.Name, library!.URL);
                hash = library!.SHA1!;
            }

            string filepath = VFS.Combine(
                MPathResolver.LibraryPath(launcherPath),
                FabricLibrary.ParsePath(library!.Name)
            );
            loader.Libraries.Add(filepath);

            if (!await Request.DownloadSHA1(request, filepath, hash))
                return false;
        }

        if (Validate.For.IsNull(launcherInstance?.FabricLoaders))
            return false;
        List<LauncherLoader> existingLoaders = [];

        foreach (LauncherLoader existingLoader in launcherInstance!.FabricLoaders!)
        {
            if (existingLoader.Version == loader.Version)
                existingLoaders.Add(existingLoader);
        }

        foreach (LauncherLoader existingLoader in existingLoaders)
            launcherInstance!.FabricLoaders!.Remove(existingLoader);

        launcherInstance!.FabricLoaders!.Add(loader);
        SettingsProvider.Load()?.Save(launcherInstance);
        return true;
    }
}
