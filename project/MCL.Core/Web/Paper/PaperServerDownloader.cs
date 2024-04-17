using System.Threading.Tasks;
using MCL.Core.Extensions.Paper;
using MCL.Core.Interfaces.Web.Paper;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Paper;
using MCL.Core.Resolvers.Paper;

namespace MCL.Core.Web.Paper;

public class PaperServerDownloader : IPaperServerDownloader<PaperBuild, PaperConfigUrls>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        PaperBuild paperBuild,
        PaperConfigUrls paperConfigUrls
    )
    {
        if (!paperConfigUrls.JarUrlExists())
            return false;

        if (!paperBuild.BuildExists())
            return false;

        string filepath = PaperPathResolver.DownloadedJarPath(launcherPath, launcherVersion);
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
