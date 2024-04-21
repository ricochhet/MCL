using System.Text;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Web;

public static class VersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MUrls mUrls)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([mUrls?.VersionManifest]))
            return false;

        string filepath = MPathResolver.VersionManifestPath(launcherPath);
        string versionManifest = await Request.GetJsonAsync<MVersionManifest>(
            mUrls.VersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (ObjectValidator<string>.IsNullOrWhiteSpace([versionManifest]))
            return false;
        return true;
    }
}
