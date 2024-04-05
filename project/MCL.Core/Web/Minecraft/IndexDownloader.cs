using System.IO;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class IndexDownloader
{
    public static async Task<bool> Download(string minecraftPath, AssetIndex assetIndex)
    {
        if (assetIndex == null || string.IsNullOrEmpty(assetIndex?.SHA1) || string.IsNullOrEmpty(assetIndex?.URL))
            return false;

        string downloadPath = Path.Combine(
            MinecraftPathResolver.AssetsPath(minecraftPath),
            "indexes",
            assetIndex.ID + ".json"
        );
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == assetIndex.SHA1)
        {
            return true;
        }
        return await Request.Download(assetIndex.URL, downloadPath);
    }
}
