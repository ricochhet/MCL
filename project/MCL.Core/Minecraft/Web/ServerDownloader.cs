using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ServerDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [versionDetails?.Downloads?.Server?.SHA1, versionDetails?.Downloads?.Server?.URL, versionDetails?.ID]
            )
        )
            return false;

        ServerProperties.NewEula(launcherPath);
        ServerProperties.NewProperties(launcherPath);

        return await Request.DownloadSHA1(
            versionDetails.Downloads.Server.URL,
            MPathResolver.ServerJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Server.SHA1
        );
    }
}
