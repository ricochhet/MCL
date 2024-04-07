using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(versionDetails))
            return false;

        ServerProperties.NewEula(minecraftPath);
        ServerProperties.NewProperties(minecraftPath);

        return await Request.Download(
            MinecraftPathResolver.ServerJarPath(minecraftPath, versionDetails),
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

        if (string.IsNullOrEmpty(versionDetails.Downloads.Server.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.Server.URL))
            return false;

        return true;
    }
}
