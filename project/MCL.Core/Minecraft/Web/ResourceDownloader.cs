using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ResourceDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MUrls mUrls, MAssetsData assets)
    {
        if (!mUrls.MinecraftResourcesExists() || !assets.ObjectsExists())
            return false;

        string objectsPath = VFS.Combine(MPathResolver.AssetsPath(launcherPath), "objects");
        foreach ((_, MAsset asset) in assets.Objects)
        {
            if (asset == null)
                continue;

            if (string.IsNullOrWhiteSpace(asset.Hash))
                return false;

            string request = $"{mUrls.MinecraftResources}/{asset.Hash[..2]}/{asset.Hash}";
            string filepath = VFS.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            if (!await Request.DownloadSHA1(request, filepath, asset.Hash))
                return false;
        }

        return true;
    }
}
