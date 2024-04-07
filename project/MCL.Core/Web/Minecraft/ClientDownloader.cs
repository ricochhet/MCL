using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class ClientDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (!Exists(minecraftPath, versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientJarPath(minecraftPath, versionDetails),
            versionDetails.Downloads.Client.URL,
            versionDetails.Downloads.Client.SHA1
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

        if (versionDetails.Downloads.Client == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.Client.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.Downloads.Client.URL))
            return false;

        return true;
    }
}
