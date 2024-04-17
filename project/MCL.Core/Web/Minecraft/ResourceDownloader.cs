using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ResourceDownloader : IResourceDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls, MCAssetsData assets)
    {
        if (!configUrls.MinecraftResourcesExists() || !assets.ObjectsExists())
            return false;

        string objectsPath = VFS.Combine(MinecraftPathResolver.AssetsPath(launcherPath), "objects");
        foreach ((_, MCAsset asset) in assets.Objects)
        {
            if (asset == null)
                continue;

            if (string.IsNullOrWhiteSpace(asset.Hash))
                return false;

            string request = $"{configUrls.MinecraftResources}/{asset.Hash[..2]}/{asset.Hash}";
            string filepath = VFS.Combine(objectsPath, asset.Hash[..2], asset.Hash);
            if (!await Request.Download(request, filepath, asset.Hash))
                return false;
        }

        return true;
    }
}
