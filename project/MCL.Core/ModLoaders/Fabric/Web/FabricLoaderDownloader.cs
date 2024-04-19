using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Extensions;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricLoaderDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        FabricProfile fabricProfile,
        FabricUrls fabricUrls
    )
    {
        if (!launcherVersion.FabricLoaderVersionExists())
            return false;

        if (
            !fabricProfile.LibraryExists()
            || !fabricUrls.ApiLoaderNameExists()
            || !fabricUrls.ApiIntermediaryNameExists()
        )
            return false;

        LauncherModLoader loader = new() { LoaderVersion = launcherVersion.FabricLoaderVersion };

        foreach (FabricLibrary library in fabricProfile.Libraries)
        {
            if (!library.LibraryExists())
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
                if (string.IsNullOrWhiteSpace(library.SHA1))
                    return false;
                request = FabricLibrary.ParseURL(library.Name, library.URL);
                hash = library.SHA1;
            }

            string filepath = VFS.Combine(
                MPathResolver.LibraryPath(launcherPath),
                FabricLibrary.ParsePath(library.Name)
            );
            loader.Libraries.Add(filepath.Replace("\\", "/"));

            if (!await Request.Download(request, filepath, hash))
                return false;
        }

        launcherInstance.FabricLoaders.Add(loader);
        SettingsService.Load().Save(launcherInstance);
        return true;
    }
}
