using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class ClientDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
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
}
