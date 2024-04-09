using System.Threading.Tasks;
using MCL.Core.Handlers.Java;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ClientDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!ClientDownloaderErr.Exists(versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Client.URL,
            versionDetails.Downloads.Client.SHA1
        );
    }
}
