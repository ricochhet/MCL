using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionManifestDownloader : IMCVersionManifestDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!VersionManifestDownloaderErr.Exists(configUrls))
            return false;

        string filepath = MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath);
        string versionManifest = await Request.GetJsonAsync<MCVersionManifest>(
            configUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(versionManifest))
            return false;
        return true;
    }
}
