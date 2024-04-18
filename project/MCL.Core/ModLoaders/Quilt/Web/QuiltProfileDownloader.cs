using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Extensions;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Web;

public static class QuiltProfileDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltUrls quiltUrls
    )
    {
        if (!launcherVersion.VersionsExists())
            return false;

        if (!quiltUrls.LoaderProfileUrlExists())
            return false;

        string quiltProfile = await Request.GetJsonAsync<QuiltProfile>(
            QuiltPathResolver.LoaderProfileUrlPath(quiltUrls, launcherVersion),
            QuiltPathResolver.DownloadedProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltProfile))
            return false;
        return true;
    }
}
