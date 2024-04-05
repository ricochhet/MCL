using System.IO;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
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

        string downloadPath = MinecraftPathResolver.ClientIndexPath(minecraftPath, versionDetails);
        if (FsProvider.Exists(downloadPath) && CryptographyHelper.Sha1(downloadPath) == versionDetails.AssetIndex.SHA1)
        {
            return true;
        }
        return await Request.Download(versionDetails.AssetIndex.URL, downloadPath);
    }
}
