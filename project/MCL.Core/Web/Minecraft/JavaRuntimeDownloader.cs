using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Minecraft;

public static class JavaRuntimeDownloader
{
    public static async Task<bool> Download(
        string minecraftPath,
        JavaRuntimeTypeEnum javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    )
    {
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
                        JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType)
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
