using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Paper;
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
        if (!MCLauncherVersion.Exists(launcherVersion))
            return false;

        if (!paperConfigUrls.VersionManifestExists())
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
