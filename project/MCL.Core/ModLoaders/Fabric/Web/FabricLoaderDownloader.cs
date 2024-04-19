using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
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
        FabricProfile fabricProfile,
        FabricUrls fabricUrls
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;

        if (
            !fabricProfile.LibraryExists()
            || !fabricUrls.ApiLoaderNameExists()
            || !fabricUrls.ApiIntermediaryNameExists()
        )
            return false;

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

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(MPathResolver.LibraryPath(launcherPath), FabricLibrary.ParsePath(library.Name)),
                    hash
                )
            )
                return false;
        }

        return true;
    }
}
