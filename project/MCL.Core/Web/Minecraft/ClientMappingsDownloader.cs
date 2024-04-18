using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ClientMappingsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftVersionDetails versionDetails)
    {
        if (!versionDetails.ClientMappingsExists())
            return false;

        return await Request.Download(
            versionDetails.Downloads.ClientMappings.URL,
            MinecraftPathResolver.ClientMappingsPath(launcherPath, versionDetails),
            versionDetails.Downloads.ClientMappings.SHA1
        );
    }
}
