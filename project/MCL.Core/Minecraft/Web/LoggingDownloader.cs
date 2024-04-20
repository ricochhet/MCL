using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class LoggingDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                versionDetails?.Logging?.Client?.File?.SHA1,
                versionDetails?.Logging?.Client?.File?.URL,
                versionDetails?.ID
            )
        )
            return false;

        return await Request.DownloadSHA1(
            versionDetails.Logging.Client.File.URL,
            MPathResolver.LoggingPath(launcherPath, versionDetails),
            versionDetails.Logging.Client.File.SHA1
        );
    }
}
