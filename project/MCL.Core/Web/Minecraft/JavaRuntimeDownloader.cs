using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class JavaRuntimeDownloader
{
    public static async Task<bool> Download(
        string minecraftPath,
        MCVersionDetails versionDetails,
        JavaRuntimeFiles javaRuntimeFiles
    )
    {
        if (versionDetails == null)
            return false;

        if (javaRuntimeFiles == null || javaRuntimeFiles?.Files.Count != 0)
            return false;

        foreach ((string path, JavaRuntimeFile javaRuntimeFile) in javaRuntimeFiles.Files)
        {
            if (javaRuntimeFile.Downloads == null)
                continue;

            if (javaRuntimeFile.Type == "file")
            {
                if (javaRuntimeFile.Downloads == null || javaRuntimeFile?.Downloads?.Raw == null)
                    return false;

                string downloadPath = Path.Combine(
                    MinecraftPathResolver.DownloadedJavaRuntimePath(
                        minecraftPath,
                        versionDetails.JavaVersion.Component
                    ),
                    path
                );
                return await Request.Download(
                    downloadPath,
                    javaRuntimeFile.Downloads.Raw.URL,
                    javaRuntimeFile.Downloads.Raw.SHA1
                );
            }
        }

        return false;
    }
}
