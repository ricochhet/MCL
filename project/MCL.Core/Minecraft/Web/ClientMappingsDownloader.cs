using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ClientMappingsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                versionDetails?.Downloads?.ClientMappings?.SHA1,
                versionDetails?.Downloads?.ClientMappings?.URL,
                versionDetails?.ID
            )
        )
            return false;

        return await Request.DownloadSHA1(
            versionDetails.Downloads.ClientMappings.URL,
            MPathResolver.ClientMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ClientMappings.SHA1
        );
    }
}
