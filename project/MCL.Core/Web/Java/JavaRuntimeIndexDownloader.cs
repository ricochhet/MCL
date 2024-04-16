using System.Text;
using System.Threading.Tasks;
using MCL.Core.Extensions.Java;
using MCL.Core.Interfaces.Web.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Web.Java;

public class JavaRuntimeIndexDownloader : IJavaRuntimeIndexDownloader
{
    public static async Task<bool> Download(MCLauncherPath launcherPath, MCConfigUrls configUrls)
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!configUrls.JavaRuntimeIndexUrlExists())
            return false;

        string filepath = JavaPathResolver.DownloadedJavaRuntimeIndexPath(launcherPath);
        string javaRuntimeIndex = await Request.GetJsonAsync<JavaRuntimeIndex>(
            configUrls.JavaRuntimeIndexUrl,
            filepath,
            Encoding.UTF8
        );
        if (string.IsNullOrWhiteSpace(javaRuntimeIndex))
            return false;
        return true;
    }
}
