using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class VersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MUrls minecraftUrls)
    {
        if (!minecraftUrls.VersionManifestExists())
            return false;

        string filepath = MPathResolver.DownloadedVersionManifestPath(launcherPath);
        string versionManifest = await Request.GetJsonAsync<MVersionManifest>(
            minecraftUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(versionManifest))
            return false;
        return true;
    }
}
