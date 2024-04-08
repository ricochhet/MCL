using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class FabricProfileDownloader : IFabricProfileDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return false;

        if (!Exists(fabricConfigUrls))
            return false;

        string fabricProfile = await Request.DoRequest(
            MinecraftFabricPathResolver.FabricLoaderProfileUrlPath(fabricConfigUrls, launcherVersion),
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrEmpty(fabricProfile))
            return false;
        return true;
    }

    public static bool Exists(MCFabricConfigUrls fabricConfigUrls)
    {
        if (fabricConfigUrls == null)
            return false;

        if (string.IsNullOrEmpty(fabricConfigUrls.FabricLoaderProfileUrl))
            return false;

        return true;
    }
}
