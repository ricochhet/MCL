using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class VersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftUrls minecraftUrls)
    {
        if (!minecraftUrls.VersionManifestExists())
            return false;

        string filepath = MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath);
        string versionManifest = await Request.GetJsonAsync<MinecraftVersionManifest>(
            minecraftUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(versionManifest))
            return false;
        return true;
    }
}
