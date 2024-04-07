using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class IndexDownloader : IMCGenericDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (!Exists(minecraftPath, versionDetails))
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientIndexPath(minecraftPath, versionDetails),
            versionDetails.AssetIndex.URL,
            versionDetails.AssetIndex.SHA1
        );
    }

    public static bool Exists(string minecraftPath, MCVersionDetails versionDetails)
    {
        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        if (versionDetails == null)
            return false;

        if (versionDetails.AssetIndex == null)
            return false;

        if (string.IsNullOrEmpty(versionDetails.AssetIndex.SHA1))
            return false;

        if (string.IsNullOrEmpty(versionDetails.AssetIndex.URL))
            return false;

        return true;
    }
}
