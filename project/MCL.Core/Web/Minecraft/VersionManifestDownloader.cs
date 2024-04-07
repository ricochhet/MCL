using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionManifestDownloader : IMCVersionManifestDownloader
{
    public static async Task<bool> Download(MCLauncherPath minecraftPath, MCConfigUrls minecraftUrls)
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(minecraftUrls))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath);
        string versionManifest = await Request.DoRequest(minecraftUrls.VersionManifest, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(versionManifest))
            return false;
        return true;
    }

    public static bool Exists(MCConfigUrls minecraftUrls)
    {
        if (minecraftUrls == null)
            return false;

        if (string.IsNullOrEmpty(minecraftUrls.VersionManifest))
            return false;

        return true;
    }
}
