using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftFabric;

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
            if (string.IsNullOrWhiteSpace(library.Name))
                return false;

            if (string.IsNullOrWhiteSpace(library.URL))
                return false;

            string url;
            string sha1;
            if (library.Name.Contains(fabricConfigUrls.FabricApiLoaderName))
            {
                url = MinecraftFabricPathResolver.FabricLoaderJarUrlPath(fabricConfigUrls, launcherVersion);
                sha1 = string.Empty;
            }
            else if (library.Name.Contains(fabricConfigUrls.FabricApiIntermediaryName))
            {
                url = MCFabricLibrary.ParseURL(library.Name, library.URL);
                sha1 = string.Empty;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(library.SHA1))
                    return false;
                url = MCFabricLibrary.ParseURL(library.Name, library.URL);
                sha1 = library.SHA1;
            }

            if (
                !await Request.Download(
                    url,
                    VFS.Combine(
                        MinecraftPathResolver.LibraryPath(launcherPath),
                        MCFabricLibrary.ParsePath(library.Name)
                    ),
                    sha1
                )
            )
                return false;
        }

        return true;
    }
}
