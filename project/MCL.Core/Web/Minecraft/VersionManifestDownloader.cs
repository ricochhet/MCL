using System.Text;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionManifestDownloader
{
    public static async Task<bool> Download(MCConfigUrls minecraftUrls, string minecraftPath)
    {
        if (minecraftUrls == null || string.IsNullOrEmpty(minecraftUrls?.VersionManifest))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath);
        string versionManifest = await Request.DoRequest(minecraftUrls.VersionManifest, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(versionManifest))
            return false;
        return true;
    }
}
