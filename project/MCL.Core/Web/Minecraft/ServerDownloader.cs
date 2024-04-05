using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ServerDownloader
{
    public static async Task<bool> Download(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPathResolver.ServerJarPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.Server.SHA1
        )
        {
            return true;
        }

        ServerProperties.NewEula(minecraftPath);
        ServerProperties.NewProperties(minecraftPath);

        return await Request.Download(versionDetails.Downloads.Server.URL, downloadPath);
    }

    public static async Task<bool> DownloadMappings(string minecraftPath, VersionDetails versionDetails)
    {
        string downloadPath = MinecraftPathResolver.ServerMappingsPath(minecraftPath, versionDetails);
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == versionDetails.Downloads.ServerMappings.SHA1)
        {
            return true;
        }

        return await Request.Download(versionDetails.Downloads.ServerMappings.URL, downloadPath);
    }
}