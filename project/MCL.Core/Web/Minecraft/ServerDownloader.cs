using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!ServerDownloaderErr.Exists(versionDetails))
            return false;

        ServerProperties.NewEula(launcherPath);
        ServerProperties.NewProperties(launcherPath);

        return await Request.Download(
            versionDetails.Downloads.Server.URL,
            MinecraftPathResolver.ServerJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Server.SHA1
        );
    }
}
