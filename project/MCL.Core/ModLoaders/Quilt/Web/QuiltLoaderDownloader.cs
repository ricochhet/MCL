using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltLoaderDownloader
{
    public static async Task<bool> Download(
        Instance instance,
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltProfile quiltProfile,
        QuiltUrls quiltUrls
    )
    {
        if (!launcherVersion.QuiltLoaderVersionExists())
            return false;

        if (!quiltProfile.LibraryExists() || !quiltUrls.ApiLoaderNameExists() || !quiltUrls.ApiIntermediaryNameExists())
            return false;

        InstanceModLoader loader = new() { LoaderVersion = launcherVersion.QuiltLoaderVersion };

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

            loader.Libraries.Add(VFS.GetFileName(library.Name));

            if (
                !await Request.Download(
                    request,
                    VFS.Combine(MPathResolver.LibraryPath(launcherPath), QuiltLibrary.ParsePath(library.Name)),
                    string.Empty
                )
            )
                return false;
        }

        instance.FabricLoaders.Add(loader);
        InstanceService.Save(instance);
        return true;
    }
}
