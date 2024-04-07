using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class LoggingDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (!Exists(minecraftPath, versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.LoggingPath(minecraftPath, versionDetails),
            versionDetails.Logging.Client.File.URL,
            versionDetails.Logging.Client.File.SHA1
        );
    }

    public static bool Exists(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        if (versionDetails == null)
            return false;

        if (versionDetails.Logging == null)
            return false;

        if (versionDetails.Logging.Client == null)
            return false;

        if (versionDetails.Logging.Client.File == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.Logging.Client.File.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Logging.Client.File.URL))
            return false;

        return true;
    }
}
