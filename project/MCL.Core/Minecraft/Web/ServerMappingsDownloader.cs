using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ServerMappingsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [
                    versionDetails?.Downloads?.ServerMappings?.SHA1,
                    versionDetails?.Downloads?.ServerMappings?.URL,
                    versionDetails?.ID
                ]
            )
        )
            return false;

        return await Request.DownloadSHA1(
            versionDetails.Downloads.ServerMappings.URL,
            MPathResolver.ServerMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
