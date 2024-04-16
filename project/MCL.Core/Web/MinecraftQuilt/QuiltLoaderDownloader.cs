using System.Threading.Tasks;
using MCL.Core.Extensions.MinecraftQuilt;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public class QuiltLoaderDownloader : IFabricLoaderDownloader<MCQuiltProfile, MCQuiltConfigUrls>
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

        if (
            !quiltProfile.LibraryExists()
            || !quiltConfigUrls.ApiLoaderNameExists()
            || !quiltConfigUrls.ApiIntermediaryNameExists()
        )
            return false;

        foreach (MCQuiltLibrary library in quiltProfile.Libraries)
        {
            if (!library.LibraryExists())
                return false;

            string request;
            if (library.Name.Contains(quiltConfigUrls.QuiltApiLoaderName))
            {
                request = QuiltPathResolver.LoaderJarUrlPath(quiltConfigUrls, launcherVersion);
            }
            else if (library.Name.Contains(quiltConfigUrls.QuiltApiIntermediaryName))
            {
                request = MCQuiltLibrary.ParseURL(library.Name, library.URL);
            }
            else
            {
                request = MCQuiltLibrary.ParseURL(library.Name, library.URL);
            }

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(
                        MinecraftPathResolver.LibraryPath(launcherPath),
                        MCQuiltLibrary.ParsePath(library.Name)
                    ),
                    string.Empty
                )
            )
                return false;
        }

        return true;
    }
}
