using System.IO;
using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ResourceDownloader : IMCResourceDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets)
    {
        if (!Exists(minecraftPath, minecraftUrls, assets))
            return false;

        string objectsPath = Path.Combine(MinecraftPathResolver.AssetsPath(minecraftPath), "objects");
        foreach ((_, MCAsset asset) in assets.Objects)
        {
            if (asset == null)
                continue;

            if (string.IsNullOrEmpty(asset.Hash))
                return false;

            string url = $"{minecraftUrls.MinecraftResources}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = Path.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            if (!await Request.Download(downloadPath, url, asset.Hash))
                return false;
        }

        return true;
    }

    public static bool Exists(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets)
    {
        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        if (assets == null)
            return false;

        if (assets.Objects == null)
            return false;

        if (minecraftUrls == null)
            return false;

        if (string.IsNullOrEmpty(minecraftUrls.MinecraftResources))
            return false;

        return true;
    }
}
