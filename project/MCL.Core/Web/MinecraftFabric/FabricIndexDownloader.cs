using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
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

        if (!MCFabricConfigUrlsErr.Exists(fabricConfigUrls))
            return false;

        string filepath = MinecraftFabricPathResolver.DownloadedFabricIndexPath(launcherPath);
        string fabricIndex = await Request.GetJsonAsync<MCFabricIndex>(
            fabricConfigUrls.FabricVersionsIndex,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricIndex))
            return false;
        return true;
    }
}
