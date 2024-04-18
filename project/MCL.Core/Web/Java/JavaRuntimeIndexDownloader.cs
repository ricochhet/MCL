using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Web.Java;

public static class JavaRuntimeIndexDownloader
{
    public static async Task<bool> Download(LauncherPath launcherPath, MinecraftUrls minecraftUrls)
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
