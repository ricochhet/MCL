using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftQuilt;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public class QuiltProfileDownloader : IFabricProfileDownloader<MCQuiltConfigUrls>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltConfigUrls quiltConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return false;

        if (!quiltConfigUrls.LoaderProfileUrlExists())
            return false;

        string quiltProfile = await Request.GetJsonAsync<MCQuiltProfile>(
            QuiltPathResolver.LoaderProfileUrlPath(quiltConfigUrls, launcherVersion),
            QuiltPathResolver.DownloadedProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltProfile))
            return false;
        return true;
    }
}
