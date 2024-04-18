using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Fabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.ModLoaders.Fabric;

namespace MCL.Core.Web.ModLoaders.Fabric;

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
            if (library.Name.Contains(fabricUrls.FabricApiLoaderName))
            {
                request = FabricPathResolver.LoaderJarUrlPath(fabricUrls, launcherVersion);
                hash = string.Empty;
            }
            else if (library.Name.Contains(fabricUrls.FabricApiIntermediaryName))
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
                    VFS.Combine(MinecraftPathResolver.LibraryPath(launcherPath), FabricLibrary.ParsePath(library.Name)),
                    hash
                )
            )
                return false;
        }

        return true;
    }
}
