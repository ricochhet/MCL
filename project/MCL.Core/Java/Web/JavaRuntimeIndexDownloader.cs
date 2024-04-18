using System.Text;
using System.Threading.Tasks;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Web;

public static class JavaRuntimeIndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MUrls minecraftUrls)
    {
        if (!minecraftUrls.JavaRuntimeIndexUrlExists())
            return false;

        string filepath = JavaPathResolver.DownloadedJavaRuntimeIndexPath(launcherPath);
        string javaRuntimeIndex = await Request.GetJsonAsync<JavaRuntimeIndex>(
            minecraftUrls.JavaRuntimeIndexUrl,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(javaRuntimeIndex))
            return false;
        return true;
    }
}
