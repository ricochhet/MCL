using System.Threading.Tasks;
using MCL.Core.Handlers.Java;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ResourceDownloader : IMCResourceDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls, MCAssetsData assets)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!MCAssetsDataErrorHandler.Exists(configUrls, assets))
            return false;

        string objectsPath = VFS.Combine(MinecraftPathResolver.AssetsPath(launcherPath), "objects");
        foreach ((_, MCAsset asset) in assets.Objects)
        {
            if (asset == null)
                continue;

            if (string.IsNullOrWhiteSpace(asset.Hash))
                return false;

            string url = $"{configUrls.MinecraftResources}/{asset.Hash[..2]}/{asset.Hash}";
            string downloadPath = VFS.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            if (!await Request.Download(downloadPath, url, asset.Hash))
                return false;
        }

        return true;
    }
}
