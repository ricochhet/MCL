using System.IO;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Providers;
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

        if (!Exists(fabricProfile, fabricConfigUrls))
            return false;

        foreach (MCFabricLibrary library in fabricProfile.Libraries)
        {
            if (string.IsNullOrEmpty(library.Name))
                return false;

            if (string.IsNullOrEmpty(library.URL))
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
                if (string.IsNullOrEmpty(library.SHA1))
                    return false;
                url = MCFabricLibrary.ParseURL(library.Name, library.URL);
                sha1 = library.SHA1;
            }

            if (
                !await Request.Download(
                    Path.Combine(
                        MinecraftPathResolver.LibraryPath(launcherPath),
                        MCFabricLibrary.ParsePath(library.Name)
                    ),
                    url,
                    sha1
                )
            )
                return false;
        }

        return true;
    }

    public static bool Exists(MCFabricProfile fabricProfile, MCFabricConfigUrls fabricConfigUrls)
    {
        if (fabricConfigUrls == null)
            return false;

        if (string.IsNullOrEmpty(fabricConfigUrls.FabricLoaderJarUrl))
            return false;

        if (fabricProfile == null)
            return false;

        if (fabricProfile.Libraries == null)
            return false;

        return true;
    }
}
