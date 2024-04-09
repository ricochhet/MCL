using System.Threading.Tasks;
using MCL.Core.Handlers.Java;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerMappingsDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!ServerMappingsDownloaderErr.Exists(versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ServerMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ServerMappings.URL,
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
