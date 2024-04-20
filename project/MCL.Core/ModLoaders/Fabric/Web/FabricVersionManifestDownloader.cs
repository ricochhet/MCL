using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Web;

public static class FabricVersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, FabricUrls fabricUrls)
    {
        if (ObjectValidator<string>.IsNullOrWhitespace(fabricUrls?.VersionManifest))
            return false;

        string filepath = FabricPathResolver.VersionManifestPath(launcherPath);
        string fabricVersionManifest = await Request.GetJsonAsync<FabricVersionManifest>(
            fabricUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(fabricVersionManifest))
            return false;
        return true;
    }
}
