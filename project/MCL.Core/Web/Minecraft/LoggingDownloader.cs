using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class LoggingDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (
            versionDetails?.Logging?.Client == null
            || string.IsNullOrEmpty(versionDetails.Logging.Client.File?.SHA1)
            || string.IsNullOrEmpty(versionDetails.Logging.Client.File?.URL)
        )
            return false;

        return await Request.Download(
            MinecraftPathResolver.LoggingPath(minecraftPath, versionDetails),
            versionDetails.Logging.Client.File.URL,
            versionDetails.Logging.Client.File.SHA1
        );
    }
}
