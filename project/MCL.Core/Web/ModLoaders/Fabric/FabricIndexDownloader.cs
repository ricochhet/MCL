using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.ModLoaders.Fabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Resolvers.ModLoaders.Fabric;

namespace MCL.Core.Web.ModLoaders.Fabric;

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
