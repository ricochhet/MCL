using System.Text;
using System.Threading.Tasks;
using MCL.Core.Handlers.MinecraftFabric;
using MCL.Core.Interfaces.Web.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Web.MinecraftFabric;

public class FabricProfileDownloader : IFabricProfileDownloader<MCFabricConfigUrls>
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return false;

        if (!FabricConfigUrlsErr.Exists(fabricConfigUrls))
            return false;

        string fabricProfile = await Request.GetJsonAsync<MCFabricProfile>(
            FabricPathResolver.LoaderProfileUrlPath(fabricConfigUrls, launcherVersion),
            FabricPathResolver.DownloadedProfilePath(launcherPath, launcherVersion),
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricProfile))
            return false;
        return true;
    }
}
