using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

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

        if (!MCFabricConfigUrlsErr.Exists(fabricConfigUrls))
            return false;

        string fabricProfile = await Request.GetJsonAsync<MCFabricProfile>(
            MinecraftFabricPathResolver.FabricLoaderProfileUrlPath(fabricConfigUrls, launcherVersion),
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricProfile))
            return false;
        return true;
    }
}
