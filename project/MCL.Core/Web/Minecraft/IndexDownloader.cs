using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class IndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftVersionDetails versionDetails)
    {
        if (!versionDetails.AssetIndexExists())
            return false;

        return await Request.Download(
            versionDetails.AssetIndex.URL,
            MinecraftPathResolver.ClientIndexPath(launcherPath, versionDetails),
            versionDetails.AssetIndex.SHA1
        );
    }
}
