using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ServerMappingsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftVersionDetails versionDetails)
    {
        if (!versionDetails.ServerMappingsExists())
            return false;

        return await Request.Download(
            versionDetails.Downloads.ServerMappings.URL,
            MinecraftPathResolver.ServerMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
