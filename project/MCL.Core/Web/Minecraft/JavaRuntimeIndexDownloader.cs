using System.Text;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class JavaRuntimeIndexDownloader
{
    public static async Task<bool> Download(string minecraftPath, MCConfigUrls minecraftUrls)
    {
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
}
