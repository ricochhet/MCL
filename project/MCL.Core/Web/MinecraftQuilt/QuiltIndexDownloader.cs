using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.MinecraftQuilt;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public class QuiltIndexDownloader : IFabricIndexDownloader<MCQuiltConfigUrls>
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCQuiltConfigUrls quiltConfigUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!quiltConfigUrls.VersionsIndexExists())
            return false;

        string filepath = QuiltPathResolver.DownloadedIndexPath(launcherPath);
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
