using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Extensions;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Web;

public static class PaperIndexDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        PaperUrls paperUrls
    )
    {
        if (!paperUrls.VersionManifestExists())
            return false;

        string filepath = PaperPathResolver.DownloadedIndexPath(launcherPath, launcherVersion);
        string paperIndex = await Request.GetJsonAsync<PaperVersionManifest>(
            string.Format(paperUrls.PaperVersionManifest, "paper", launcherVersion.Version),
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(paperIndex))
            return false;
        return true;
    }
}
