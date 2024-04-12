using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftQuilt;
using MCL.Core.Interfaces.MinecraftQuilt;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public class QuiltIndexDownloader : IQuiltIndexDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCQuiltConfigUrls quiltConfigUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!MCQuiltConfigUrlsErr.Exists(quiltConfigUrls))
            return false;

        string filepath = MinecraftQuiltPathResolver.DownloadedQuiltIndexPath(launcherPath);
        string quiltIndex = await Request.GetJsonAsync<MCQuiltIndex>(
            quiltConfigUrls.QuiltVersionsIndex,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(quiltIndex))
            return false;
        return true;
    }
}
