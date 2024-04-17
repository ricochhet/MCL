using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerDownloader : IGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        if (!versionDetails.ServerExists())
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
