using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ServerMappingsDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (!Exists(minecraftPath, versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ServerMappingsPath(minecraftPath, versionDetails),
            versionDetails.Downloads.ServerMappings.URL,
            versionDetails.Downloads.ServerMappings.SHA1
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

        if (versionDetails.Downloads.ServerMappings == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.ServerMappings.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.ServerMappings.URL))
            return false;

        return true;
    }
}
