using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ClientDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (!versionDetails.ClientExists())
            return false;

        return await Request.DownloadSHA1(
            versionDetails.Downloads.Client.URL,
            MPathResolver.ClientJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Client.SHA1
        );
    }
}
