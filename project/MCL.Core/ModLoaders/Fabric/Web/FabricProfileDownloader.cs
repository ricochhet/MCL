using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricProfileDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricUrls fabricUrls
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                launcherVersion?.Version,
                launcherVersion?.FabricLoaderVersion,
                fabricUrls?.LoaderProfile
            )
        )
            return false;

        string fabricProfile = await Request.GetJsonAsync<FabricProfile>(
            FabricPathResolver.LoaderProfilePath(fabricUrls, launcherVersion),
            FabricPathResolver.ProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricProfile))
            return false;
        return true;
    }
}
