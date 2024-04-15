using System.Threading.Tasks;
using MCL.Core.Handlers.Paper;
using MCL.Core.Interfaces.Web.Paper;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Paper;
using MCL.Core.Resolvers.Paper;

namespace MCL.Core.Web.Paper;

public class PaperServerDownloader : IPaperServerDownloader<PaperVersionManifest, PaperConfigUrls>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        PaperVersionManifest paperVersionManifest,
        PaperConfigUrls paperConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!PaperConfigUrlsErr.Exists(paperConfigUrls))
            return false;

        if (!PaperServerDownloaderErr.Exists(paperVersionManifest))
            return false;

        PaperBuild paperBuild = paperVersionManifest.Builds[^1];
        string filepath = PaperPathResolver.DownloadedJarPath(
            launcherPath,
            launcherVersion,
            paperBuild.Downloads.Application
        );
        if (
            !await Request.Download(
                string.Format(
                    paperConfigUrls.PaperJarUrl,
                    "paper",
                    launcherVersion.Version,
                    paperBuild.Build.ToString(),
                    paperBuild.Downloads.Application.Name
                ),
                filepath,
                string.Empty // Paper only provides SHA256 throughout the API, not SHA1.
            )
        )
            return false;
        return true;
    }
}
