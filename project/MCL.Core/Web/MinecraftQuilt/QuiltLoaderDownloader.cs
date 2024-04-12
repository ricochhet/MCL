using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Web.MinecraftQuilt;

namespace MCL.Core.Web.Minecraft;

public class QuiltLoaderDownloader : IQuiltLoaderDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltProfile quiltProfile,
        MCQuiltConfigUrls quiltConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!QuiltLoaderDownloaderErr.Exists(quiltProfile, quiltConfigUrls))
            return false;

        foreach (MCQuiltLibrary library in quiltProfile.Libraries)
        {
            if (!QuiltLoaderDownloaderErr.Exists(library))
                return false;

            string request;
            string hash;
            if (library.Name.Contains(quiltConfigUrls.QuiltApiLoaderName))
            {
                request = MinecraftQuiltPathResolver.QuiltLoaderJarUrlPath(quiltConfigUrls, launcherVersion);
                hash = string.Empty;
            }
            else if (library.Name.Contains(quiltConfigUrls.QuiltApiIntermediaryName))
            {
                request = MCQuiltLibrary.ParseURL(library.Name, library.URL);
                hash = string.Empty;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(library.SHA1))
                    return false;
                request = MCQuiltLibrary.ParseURL(library.Name, library.URL);
                hash = library.SHA1;
            }

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(
                        MinecraftPathResolver.LibraryPath(launcherPath),
                        MCQuiltLibrary.ParsePath(library.Name)
                    ),
                    hash
                )
            )
                return false;
        }

        return true;
    }
}
