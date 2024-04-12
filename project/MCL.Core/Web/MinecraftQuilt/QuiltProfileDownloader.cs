using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftQuilt;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Web.Minecraft;

public class QuiltProfileDownloader : IQuiltProfileDownloader
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

        if (!MCQuiltConfigUrlsErr.Exists(quiltConfigUrls))
            return false;

        string quiltProfile = await Request.GetJsonAsync<MCQuiltProfile>(
            MinecraftQuiltPathResolver.QuiltLoaderProfileUrlPath(quiltConfigUrls, launcherVersion),
            MinecraftQuiltPathResolver.DownloadedQuiltProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltProfile))
            return false;
        return true;
    }
}
