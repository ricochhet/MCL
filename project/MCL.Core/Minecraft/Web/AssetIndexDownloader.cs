using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class AssetIndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [versionDetails?.AssetIndex?.SHA1, versionDetails?.AssetIndex?.URL, versionDetails?.Assets]
            )
        )
            return false;

        return await Request.DownloadSHA1(
            versionDetails.AssetIndex.URL,
            MPathResolver.ClientIndexPath(launcherPath, versionDetails),
            versionDetails.AssetIndex.SHA1
        );
    }
}
