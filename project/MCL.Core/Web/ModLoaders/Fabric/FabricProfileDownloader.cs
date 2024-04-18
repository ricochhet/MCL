using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Fabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Resolvers.ModLoaders.Fabric;

namespace MCL.Core.Web.ModLoaders.Fabric;

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
