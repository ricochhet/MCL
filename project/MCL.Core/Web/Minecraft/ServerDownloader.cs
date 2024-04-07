using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (!Exists(minecraftPath, versionDetails))
            return false;

        ServerProperties.NewEula(minecraftPath);
        ServerProperties.NewProperties(minecraftPath);

        return await Request.Download(
            MinecraftPathResolver.ServerJarPath(minecraftPath, versionDetails),
            versionDetails.Downloads.Server.URL,
            versionDetails.Downloads.Server.SHA1
        );
    }

    public static bool Exists(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.Server == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.Server.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.Server.URL))
            return false;

        return true;
    }
}
