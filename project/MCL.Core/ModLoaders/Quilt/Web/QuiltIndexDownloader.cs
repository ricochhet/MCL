using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltIndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, QuiltUrls quiltUrls)
    {
        if (!quiltUrls.VersionsIndexExists())
            return false;

        string filepath = QuiltPathResolver.DownloadedIndexPath(launcherPath);
        string quiltIndex = await Request.GetJsonAsync<QuiltIndex>(
            quiltUrls.QuiltVersionsIndex,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltIndex))
            return false;
        return true;
    }
}
