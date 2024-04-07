using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ClientMappingsDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientMappingsPath(minecraftPath, versionDetails),
            versionDetails.Downloads.ClientMappings.URL,
            versionDetails.Downloads.ClientMappings.SHA1
        );
    }

    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.ClientMappings == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.ClientMappings.URL))
            return false;

        return true;
    }
}
