using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltVersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, QuiltUrls quiltUrls)
    {
        if (!quiltUrls.VersionManifestExists())
            return false;

        string filepath = QuiltPathResolver.VersionManifestPath(launcherPath);
        string quiltVersionManifest = await Request.GetJsonAsync<QuiltVersionManifest>(
            quiltUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltVersionManifest))
            return false;
        return true;
    }
}