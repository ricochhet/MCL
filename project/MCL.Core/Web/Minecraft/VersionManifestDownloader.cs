using System.Text;
using System.Threading.Tasks;
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

        if (!Exists(configUrls))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath);
        string versionManifest = await Request.DoRequest(configUrls.VersionManifest, downloadPath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(versionManifest))
            return false;
        return true;
    }

    public static bool Exists(MCConfigUrls configUrls)
    {
        if (configUrls == null)
            return false;

        if (string.IsNullOrWhiteSpace(configUrls.VersionManifest))
            return false;

        return true;
    }
}
