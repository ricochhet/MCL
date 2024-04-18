using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class VersionDetailsDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MVersion version)
    {
        if (!version.UrlExists() || !version.IdExists())
            return false;

        string filepath = MPathResolver.VersionDetailsPath(launcherPath, version);
        string versionDetails = await Request.GetJsonAsync<MVersionDetails>(version.URL, filepath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(versionDetails))
            return false;
        return true;
    }
}
