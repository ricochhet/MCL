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

        string downloadPath = MinecraftPathResolver.ClientJarPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Client.SHA1
        )
        {
            return true;
        }
        return await Request.Download(versionDetails.Downloads.Client.URL, downloadPath);
    }

    public static async Task<bool> DownloadMappings(string minecraftPath, VersionDetails versionDetails)
    {
        if (
            versionDetails?.Downloads?.ClientMappings == null
            || string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings?.URL)
        )
            return false;

        string downloadPath = MinecraftPathResolver.ClientMappingsPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.ClientMappings.SHA1
        )
        {
            return true;
        }

        return await Request.Download(versionDetails.Downloads.ClientMappings.URL, downloadPath);
    }
}
