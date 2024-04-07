using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ClientMappingsDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
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
