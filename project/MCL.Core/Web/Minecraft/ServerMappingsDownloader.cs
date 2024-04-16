using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerMappingsDownloader : IGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!versionDetails.ServerMappingsExists())
            return false;

        return await Request.Download(
            versionDetails.Downloads.ServerMappings.URL,
            MinecraftPathResolver.ServerMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
