using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.Paper;
using MCL.Core.Interfaces.Web.Paper;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Paper;
using MCL.Core.Resolvers.Paper;

namespace MCL.Core.Web.Paper;

public class PaperIndexDownloader : IPaperIndexDownloader<PaperConfigUrls>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        PaperConfigUrls paperConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!PaperConfigUrlsErr.Exists(paperConfigUrls))
            return false;

        string filepath = PaperPathResolver.DownloadedIndexPath(launcherPath, launcherVersion);
        string paperIndex = await Request.GetJsonAsync<PaperVersionManifest>(
            string.Format(paperConfigUrls.PaperVersionManifest, "paper", launcherVersion.Version),
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(paperIndex))
            return false;
        return true;
    }
}
