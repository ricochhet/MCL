using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Interfaces.Web.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public class VersionDetailsDownloader : IVersionDetailsDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCVersion version)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!version.UrlExists() || !version.IdExists())
            return false;

        string filepath = MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version);
        string versionDetails = await Request.GetJsonAsync<MCVersionDetails>(version.URL, filepath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(versionDetails))
            return false;
        return true;
    }
}
