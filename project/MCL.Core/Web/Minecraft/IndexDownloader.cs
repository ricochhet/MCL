using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class IndexDownloader
{
    public static async Task<bool> Download(string minecraftPath, VersionDetails versionDetails)
    {
        if (
            versionDetails?.AssetIndex == null
            || string.IsNullOrEmpty(versionDetails.AssetIndex?.SHA1)
            || string.IsNullOrEmpty(versionDetails.AssetIndex?.URL)
        )
            return false;

        return await Request.Download(
            MinecraftPathResolver.ClientIndexPath(minecraftPath, versionDetails),
            versionDetails.AssetIndex.URL,
            versionDetails.AssetIndex.SHA1
        );
    }
}
