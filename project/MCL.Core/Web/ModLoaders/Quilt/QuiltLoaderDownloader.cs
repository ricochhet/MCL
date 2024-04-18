using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Quilt;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Resolvers.ModLoaders.Quilt;

namespace MCL.Core.Web.ModLoaders.Quilt;

public static class QuiltLoaderDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltProfile quiltProfile,
        QuiltUrls quiltUrls
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;

        if (!quiltProfile.LibraryExists() || !quiltUrls.ApiLoaderNameExists() || !quiltUrls.ApiIntermediaryNameExists())
            return false;

        foreach (QuiltLibrary library in quiltProfile.Libraries)
        {
            if (!library.LibraryExists())
                return false;

            string request;
            if (library.Name.Contains(quiltUrls.QuiltApiLoaderName))
            {
                request = QuiltPathResolver.LoaderJarUrlPath(quiltUrls, launcherVersion);
            }
            else if (library.Name.Contains(quiltUrls.QuiltApiIntermediaryName))
            {
                request = QuiltLibrary.ParseURL(library.Name, library.URL);
            }
            else
            {
                request = QuiltLibrary.ParseURL(library.Name, library.URL);
            }

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(MinecraftPathResolver.LibraryPath(launcherPath), QuiltLibrary.ParsePath(library.Name)),
                    string.Empty
                )
            )
                return false;
        }

        return true;
    }
}
