using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Web;

public static class PaperVersionManifestDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        PaperUrls paperUrls
    )
    {
        if (ObjectValidator<string>.IsNullOrWhitespace(launcherVersion?.Version, paperUrls?.VersionManifest))
            return false;

        string filepath = PaperPathResolver.VersionManifestPath(launcherPath, launcherVersion);
        string paperIndex = await Request.GetJsonAsync<PaperVersionManifest>(
            string.Format(paperUrls.VersionManifest, "paper", launcherVersion.Version),
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(paperIndex))
            return false;
        return true;
    }
}
