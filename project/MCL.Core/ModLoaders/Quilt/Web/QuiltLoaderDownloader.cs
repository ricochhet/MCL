using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

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

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(MPathResolver.LibraryPath(launcherPath), QuiltLibrary.ParsePath(library.Name)),
                    string.Empty
                )
            )
                return false;
        }

        return true;
    }
}
