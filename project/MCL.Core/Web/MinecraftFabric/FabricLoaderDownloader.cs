using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Web.MinecraftFabric;

namespace MCL.Core.Web.Minecraft;

public class FabricLoaderDownloader : IFabricLoaderDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricProfile fabricProfile,
        MCFabricConfigUrls fabricConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!FabricLoaderDownloaderErr.Exists(fabricProfile, fabricConfigUrls))
            return false;

        foreach (MCFabricLibrary library in fabricProfile.Libraries)
        {
            if (!FabricLoaderDownloaderErr.Exists(library))
                return false;

            string request;
            string hash;
            if (library.Name.Contains(fabricConfigUrls.FabricApiLoaderName))
            {
                request = MinecraftFabricPathResolver.FabricLoaderJarUrlPath(fabricConfigUrls, launcherVersion);
                hash = string.Empty;
            }
            else if (library.Name.Contains(fabricConfigUrls.FabricApiIntermediaryName))
            {
                request = MCFabricLibrary.ParseURL(library.Name, library.URL);
                hash = string.Empty;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(library.SHA1))
                    return false;
                request = MCFabricLibrary.ParseURL(library.Name, library.URL);
                hash = library.SHA1;
            }

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(
                        MinecraftPathResolver.LibraryPath(launcherPath),
                        MCFabricLibrary.ParsePath(library.Name)
                    ),
                    hash
                )
            )
                return false;
        }

        return true;
    }
}
