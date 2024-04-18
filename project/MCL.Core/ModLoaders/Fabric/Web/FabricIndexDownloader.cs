using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Extensions;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricIndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, FabricUrls fabricUrls)
    {
        if (!fabricUrls.VersionsIndexExists())
            return false;

        string filepath = FabricPathResolver.DownloadedIndexPath(launcherPath);
        string fabricIndex = await Request.GetJsonAsync<FabricIndex>(
            fabricUrls.FabricVersionsIndex,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricIndex))
            return false;
        return true;
    }
}
