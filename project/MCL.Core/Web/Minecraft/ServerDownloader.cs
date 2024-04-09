using System.Threading.Tasks;
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

        if (!Exists(versionDetails))
            return false;

        ServerProperties.NewEula(launcherPath);
        ServerProperties.NewProperties(launcherPath);

        return await Request.Download(
            MinecraftPathResolver.ServerJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Server.URL,
            versionDetails.Downloads.Server.SHA1
        );
    }

    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.Server == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Server.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Server.URL))
            return false;

        return true;
    }
}
