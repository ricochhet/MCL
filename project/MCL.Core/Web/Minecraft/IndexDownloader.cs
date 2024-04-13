using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class IndexDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!IndexDownloaderErr.Exists(versionDetails))
            return false;

        return await Request.Download(
            versionDetails.AssetIndex.URL,
            MinecraftPathResolver.ClientIndexPath(launcherPath, versionDetails),
            versionDetails.AssetIndex.SHA1
        );
    }
}
