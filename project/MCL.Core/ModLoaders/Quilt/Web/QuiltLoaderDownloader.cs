using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltLoaderDownloader
{
#pragma warning disable IDE0079
#pragma warning disable S3776
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        QuiltProfile quiltProfile,
        QuiltUrls quiltUrls
    )
#pragma warning restore
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                launcherVersion?.QuiltLoaderVersion,
                quiltUrls?.ApiLoaderName,
                quiltUrls?.ApiIntermediaryName
            ) || ObjectValidator<List<QuiltLibrary>>.IsNullOrEmpty(quiltProfile?.Libraries)
        )
            return false;

        LauncherModLoader loader = new() { Version = launcherVersion.QuiltLoaderVersion };

        foreach (QuiltLibrary library in quiltProfile.Libraries)
        {
            if (ObjectValidator<string>.IsNullOrWhitespace(library?.Name, library?.URL))
                return false;

            string request;
            if (library.Name.Contains(quiltUrls.ApiLoaderName))
            {
                request = QuiltPathResolver.LoaderJarPath(quiltUrls, launcherVersion);
            }
            else if (library.Name.Contains(quiltUrls.ApiIntermediaryName))
            {
                request = QuiltLibrary.ParseURL(library.Name, library.URL);
            }
            else
            {
                request = QuiltLibrary.ParseURL(library.Name, library.URL);
            }

            string filepath = VFS.Combine(
                MPathResolver.LibraryPath(launcherPath),
                QuiltLibrary.ParsePath(library.Name)
            );
            loader.Libraries.Add(filepath.Replace("\\", "/"));

            if (!await Request.DownloadSHA1(request, filepath, string.Empty))
                return false;
        }

        foreach (LauncherModLoader existingLoader in launcherInstance.QuiltLoaders)
        {
            if (existingLoader.Version == loader.Version)
                launcherInstance.QuiltLoaders.Remove(existingLoader);
        }

        launcherInstance.QuiltLoaders.Add(loader);
        SettingsService.Load().Save(launcherInstance);
        return true;
    }
}
