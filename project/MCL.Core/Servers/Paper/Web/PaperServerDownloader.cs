using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Extensions;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Web;

public static class PaperServerDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        PaperBuild paperBuild,
        PaperUrls paperUrls
    )
    {
        if (!paperUrls.JarUrlExists())
            return false;

        if (!paperBuild.BuildExists())
            return false;

        string filepath = PaperPathResolver.DownloadedJarPath(launcherPath, launcherVersion);
        if (
            !await Request.Download(
                string.Format(
                    paperUrls.PaperJarUrl,
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
