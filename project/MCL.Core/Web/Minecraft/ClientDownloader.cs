using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ClientDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftVersionDetails versionDetails)
    {
        if (!versionDetails.ClientExists())
            return false;

        return await Request.Download(
            versionDetails.Downloads.Client.URL,
            MinecraftPathResolver.ClientJarPath(launcherPath, versionDetails),
            versionDetails.Downloads.Client.SHA1
        );
    }
}
