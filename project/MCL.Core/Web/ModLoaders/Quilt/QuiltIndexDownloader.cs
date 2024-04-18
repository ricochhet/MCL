using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.ModLoaders.Quilt;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Resolvers.ModLoaders.Quilt;

namespace MCL.Core.Web.ModLoaders.Quilt;

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
