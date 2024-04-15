using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftFabric;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Web.MinecraftFabric;

public class FabricIndexDownloader : IFabricIndexDownloader<MCFabricConfigUrls>
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCFabricConfigUrls fabricConfigUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!FabricConfigUrlsErr.Exists(fabricConfigUrls))
            return false;

        string filepath = FabricPathResolver.DownloadedIndexPath(launcherPath);
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
