using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class ServerMappingsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersionDetails versionDetails)
    {
        if (!versionDetails.ServerMappingsExists())
            return false;

        return await Request.Download(
            versionDetails.Downloads.ServerMappings.URL,
            MPathResolver.ServerMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
