using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class LoggingDownloader
{
    public static async Task<bool> Download(string minecraftPath, VersionDetails versionDetails)
    {
        if (
            versionDetails?.Logging?.Client == null
            || string.IsNullOrEmpty(versionDetails.Logging.Client.File?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Logging.Client.File?.URL)
        )
            return false;

        string downloadPath = MinecraftPathResolver.LoggingPath(minecraftPath, versionDetails);
        if (
            FsProvider.Exists(downloadPath)
            && CryptographyHelper.Sha1(downloadPath) == versionDetails.Logging.Client.File.SHA1
        )
        {
            return true;
        }
        return await Request.Download(versionDetails.Logging.Client.File.URL, downloadPath);
    }
}
