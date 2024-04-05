using System.IO;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ResourceDownloader
{
    public static async Task<bool> Download(
        string minecraftPath,
        string minecraftResourcesUrl,
        AssetsData assets
    )
    {
        string objectsPath = Path.Combine(MinecraftPathResolver.AssetsPath(minecraftPath), "objects");
        foreach ((_, Asset asset) in assets.Objects)
        {
            string url = $"{minecraftResourcesUrl}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = Path.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            return await Request.NewDownloadRequest(downloadPath, url, asset.Hash);
        }

        return true;
    }
}