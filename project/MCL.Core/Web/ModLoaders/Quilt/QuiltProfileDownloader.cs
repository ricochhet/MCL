using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.ModLoaders.Quilt;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Resolvers.ModLoaders.Quilt;

namespace MCL.Core.Web.ModLoaders.Quilt;

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
