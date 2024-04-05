using System.IO;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ResourceDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls, MCAssetsData assets)
    {
        if (assets == null || assets?.Objects == null || minecraftUrls == null)
            return false;

        string objectsPath = Path.Combine(MinecraftPathResolver.AssetsPath(minecraftPath), "objects");
        foreach ((_, MCAsset asset) in assets.Objects)
        {
            if (string.IsNullOrEmpty(asset.Hash) || string.IsNullOrEmpty(minecraftUrls.MinecraftResources))
                return false;

            string url = $"{minecraftUrls.MinecraftResources}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = Path.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            return await Request.Download(downloadPath, url, asset.Hash);
        }

        return true;
    }
}
