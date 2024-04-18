using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Extensions;
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
        if (!launcherVersion.VersionsExists())
            return false;

        if (!fabricUrls.LoaderProfileUrlExists())
            return false;

        string fabricProfile = await Request.GetJsonAsync<FabricProfile>(
            FabricPathResolver.LoaderProfileUrlPath(fabricUrls, launcherVersion),
            FabricPathResolver.DownloadedProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricProfile))
            return false;
        return true;
    }
}
