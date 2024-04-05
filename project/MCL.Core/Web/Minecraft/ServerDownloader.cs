using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ServerDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (
            versionDetails?.Downloads?.Server == null
            || string.IsNullOrEmpty(versionDetails.Downloads.Server?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Downloads.Server?.URL)
        )
            return false;

        ServerProperties.NewEula(minecraftPath);
        ServerProperties.NewProperties(minecraftPath);

        return await Request.Download(
            MinecraftPathResolver.ServerJarPath(minecraftPath, versionDetails),
            versionDetails.Downloads.Server.URL,
            versionDetails.Downloads.Server.SHA1
        );
    }

    public static async Task<bool> DownloadMappings(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (
            versionDetails?.Downloads?.ServerMappings == null
            || string.IsNullOrEmpty(versionDetails.Downloads.ServerMappings?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Downloads.ServerMappings?.URL)
        )
            return false;

        return await Request.Download(
            MinecraftPathResolver.ServerMappingsPath(minecraftPath, versionDetails),
            versionDetails.Downloads.ServerMappings.URL,
            versionDetails.Downloads.ServerMappings.SHA1
        );
    }
}
