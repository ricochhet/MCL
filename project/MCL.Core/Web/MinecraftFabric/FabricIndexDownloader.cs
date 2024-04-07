using System.Text;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class FabricIndexDownloader
{
    public static async Task<bool> Download(string fabricPath, MCFabricConfigUrls fabricUrls)
    {
        if (fabricUrls == null || string.IsNullOrEmpty(fabricUrls?.FabricVersionsIndex))
            return false;

        string downloadPath = MinecraftFabricPathResolver.DownloadedFabricIndexPath(fabricPath);
        string fabricIndex = await Request.DoRequest(fabricUrls.FabricVersionsIndex, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(fabricIndex))
            return false;
        return true;
    }
}
