using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class FabricIndexDownloader : IFabricIndexDownloader
{
    public static async Task<bool> Download(string fabricPath, MCFabricConfigUrls fabricUrls)
    {
        if (!Exists(fabricPath, fabricUrls))
            return false;

        string downloadPath = MinecraftFabricPathResolver.DownloadedFabricIndexPath(fabricPath);
        string fabricIndex = await Request.DoRequest(fabricUrls.FabricVersionsIndex, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(fabricIndex))
            return false;
        return true;
    }

    public static bool Exists(string fabricPath, MCFabricConfigUrls fabricUrls)
    {
        if (string.IsNullOrEmpty(fabricPath))
            return false;

        if (fabricUrls == null)
            return false;

        if (string.IsNullOrEmpty(fabricUrls.FabricVersionsIndex))
            return false;

        return true;
    }
}
