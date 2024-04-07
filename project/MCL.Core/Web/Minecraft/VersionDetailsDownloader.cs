using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionDetailsDownloader : IMCVersionDetailsDownloader
{
    public static async Task<bool> Download(MCLauncherPath minecraftPath, MCVersion version)
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(version))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version);
        string versionDetails = await Request.DoRequest(version.URL, downloadPath, Encoding.UTF8);
        if (string.IsNullOrEmpty(versionDetails))
            return false;
        return true;
    }

    public static bool Exists(MCVersion version)
    {
        if (version == null)
            return false;

        if (string.IsNullOrEmpty(version.URL))
            return false;

        return true;
    }
}
