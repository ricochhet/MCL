using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ClientDownloader
{
    public static async Task<bool> Download(string minecraftPath, VersionDetails versionDetails)
    {
        if (
            versionDetails?.Downloads?.Client == null
            || string.IsNullOrEmpty(versionDetails.Downloads.Client?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Downloads.Client?.URL)
        )
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientJarPath(minecraftPath, versionDetails),
            versionDetails.Downloads.Client.URL,
            versionDetails.Downloads.Client.SHA1
        );
    }

    public static async Task<bool> DownloadMappings(string minecraftPath, VersionDetails versionDetails)
    {
        if (
            versionDetails?.Downloads?.ClientMappings == null
            || string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings?.URL)
        )
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientMappingsPath(minecraftPath, versionDetails),
            versionDetails.Downloads.ClientMappings.URL,
            versionDetails.Downloads.ClientMappings.SHA1
        );
    }
}
