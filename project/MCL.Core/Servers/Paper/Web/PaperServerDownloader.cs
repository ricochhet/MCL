using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Helpers;
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
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [
                    paperUrls?.PaperJar,
                    paperBuild?.Build.ToString(),
                    paperBuild?.Downloads?.Application?.Name,
                    paperBuild?.Downloads?.Application?.SHA256
                ]
            )
        )
            return false;

        PaperServerProperties.NewEula(launcherPath, launcherVersion);
        PaperServerProperties.NewProperties(launcherPath, launcherVersion);

        string filepath = PaperPathResolver.JarPath(launcherPath, launcherVersion);
        if (
            !await Request.DownloadSHA256(
                string.Format(
                    paperUrls.PaperJar,
                    "paper",
                    launcherVersion.Version,
                    paperBuild.Build.ToString(),
                    paperBuild.Downloads.Application.Name
                ),
                filepath,
                paperBuild.Downloads.Application.SHA256
            )
        )
            return false;
        return true;
    }
}
