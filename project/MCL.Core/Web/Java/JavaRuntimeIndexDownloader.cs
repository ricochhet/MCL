using System.Text;
using System.Threading.Tasks;
using MCL.Core.Interfaces.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Java;

public class JavaRuntimeIndexDownloader : IJavaRuntimeIndexDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls)
    {
        if (!Exists(minecraftPath, minecraftUrls))
            return false;

        string downloadPath = MinecraftPathResolver.DownloadedJavaRuntimeIndexPath(minecraftPath);
        string javaRuntimeIndex = await Request.DoRequest(
            minecraftUrls.JavaRuntimeIndexUrl,
            downloadPath,
            Encoding.UTF8
        );
        if (string.IsNullOrEmpty(javaRuntimeIndex))
            return false;
        return true;
    }

    public static bool Exists(string minecraftPath, MCConfigUrls minecraftUrls)
    {
        if (string.IsNullOrEmpty(minecraftPath))
            return false;

        if (minecraftUrls == null)
            return false;

        if (string.IsNullOrEmpty(minecraftUrls.JavaRuntimeIndexUrl))
            return false;

        return true;
    }
}
