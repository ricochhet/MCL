using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class LoggingDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftVersionDetails versionDetails)
    {
        if (!versionDetails.LoggingExists())
            return false;

        return await Request.Download(
            versionDetails.Logging.Client.File.URL,
            MinecraftPathResolver.LoggingPath(launcherPath, versionDetails),
            versionDetails.Logging.Client.File.SHA1
        );
    }
}
