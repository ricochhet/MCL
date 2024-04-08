using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Web.Minecraft;

public class FabricIndexDownloader : IFabricIndexDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCFabricConfigUrls fabricConfigUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!Exists(fabricConfigUrls))
            return false;

        string downloadPath = MinecraftFabricPathResolver.DownloadedFabricIndexPath(launcherPath);
        string fabricIndex = await Request.DoRequest(fabricConfigUrls.FabricVersionsIndex, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(fabricIndex))
            return false;
        return true;
    }

    public static bool Exists(MCFabricConfigUrls fabricConfigUrls)
    {
        if (fabricConfigUrls == null)
            return false;

        if (string.IsNullOrEmpty(fabricConfigUrls.FabricVersionsIndex))
            return false;

        return true;
    }
}
