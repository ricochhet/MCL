using System.Text;
using System.Threading.Tasks;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Web;

public static class JavaVersionManifestDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MUrls mUrls)
    {
        if (!mUrls.JavaVersionManifestExists())
            return false;

        string filepath = JavaPathResolver.DownloadedJavaVersionManifestPath(launcherPath);
        string javaVersionManifest = await Request.GetJsonAsync<JavaVersionManifest>(
            mUrls.JavaVersionManifest,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(javaVersionManifest))
            return false;
        return true;
    }
}