using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionManifestDownloader
{
    public static async Task<bool> Download(MinecraftUrls minecraftUrls, string minecraftPath)
    {
        if (minecraftUrls == null || string.IsNullOrEmpty(minecraftUrls?.VersionManifest))
            return false;

        return await Request.Download(
            minecraftUrls.VersionManifest,
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );
    }
}
