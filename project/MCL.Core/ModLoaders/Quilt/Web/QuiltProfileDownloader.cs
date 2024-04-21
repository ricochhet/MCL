using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
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
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [launcherVersion?.Version, launcherVersion?.QuiltLoaderVersion, quiltUrls?.LoaderProfile]
            )
        )
            return false;

        string quiltProfile = await Request.GetJsonAsync<QuiltProfile>(
            QuiltPathResolver.LoaderProfilePath(quiltUrls, launcherVersion),
            QuiltPathResolver.ProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (ObjectValidator<string>.IsNullOrWhiteSpace([quiltProfile]))
            return false;
        return true;
    }
}
