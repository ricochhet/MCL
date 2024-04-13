using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionDetailsDownloader : IMCVersionDetailsDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersion version)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!VersionDetailsDownloaderErr.Exists(version))
            return false;

        string filepath = MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version);
        string versionDetails = await Request.GetJsonAsync<MCVersionDetails>(version.URL, filepath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(versionDetails))
            return false;
        return true;
    }
}
