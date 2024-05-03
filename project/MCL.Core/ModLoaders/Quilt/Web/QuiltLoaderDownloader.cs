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
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Validation.Validators;
using MCL.Core.MiniCommon.Web;
using MCL.Core.ModLoaders.Interfaces.Web;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public class QuiltLoaderDownloader : IModLoaderLoaderDownloader<QuiltProfile, QuiltUrls>
{
#pragma warning disable S3776
    /// <inheritdoc />
    public static async Task<bool> Download(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        QuiltProfile? quiltProfile,
        QuiltUrls? quiltUrls
    )
#pragma warning restore S3776
    {
        if (
            StringValidator.IsNullOrWhiteSpace(
                [launcherVersion?.QuiltLoaderVersion, quiltUrls?.ApiLoaderName, quiltUrls?.ApiIntermediaryName]
            ) || ListValidator.IsNullOrEmpty(quiltProfile?.Libraries)
        )
        {
            return false;
        }

        LauncherLoader loader = new() { Version = launcherVersion!.QuiltLoaderVersion };

        foreach (QuiltLibrary library in quiltProfile!.Libraries!)
        {
            if (StringValidator.IsNullOrWhiteSpace([library?.Name, library?.URL]))
                return false;

            string request;
            if (library!.Name.Contains(quiltUrls!.ApiLoaderName))
            {
                request = QuiltPathResolver.LoaderJarPath(quiltUrls, launcherVersion);
            }
            else if (library!.Name.Contains(quiltUrls!.ApiIntermediaryName))
            {
                request = QuiltLibrary.ParseURL(library.Name, library.URL);
            }
            else
            {
                request = QuiltLibrary.ParseURL(library!.Name, library!.URL);
            }

            string filepath = VFS.Combine(
                MPathResolver.LibraryPath(launcherPath),
                QuiltLibrary.ParsePath(library!.Name)
            );
            loader.Libraries.Add(filepath);

            if (!await Request.DownloadSHA1(request, filepath, string.Empty))
                return false;
        }

        if (ClassValidator.IsNull(launcherInstance?.QuiltLoaders))
            return false;
        List<LauncherLoader> existingLoaders = [];

        foreach (LauncherLoader existingLoader in launcherInstance!.QuiltLoaders!)
        {
            if (existingLoader.Version == loader.Version)
                launcherInstance!.QuiltLoaders!.Remove(existingLoader);
        }

        foreach (LauncherLoader existingLoader in existingLoaders)
            launcherInstance!.QuiltLoaders!.Remove(existingLoader);

        launcherInstance!.QuiltLoaders!.Add(loader);
        SettingsProvider.Load()?.Save(launcherInstance);
        return true;
    }
}
