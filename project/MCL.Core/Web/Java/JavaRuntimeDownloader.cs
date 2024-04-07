using System.IO;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Interfaces.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Java;

public class JavaRuntimeDownloader : IJavaRuntimeDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath minecraftPath,
        JavaRuntimeTypeEnum javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return false;

        if (!Exists(javaRuntimeFiles))
            return false;

        foreach ((string path, JavaRuntimeFile javaRuntimeFile) in javaRuntimeFiles.Files)
        {
            if (javaRuntimeFile.Downloads == null)
                continue;

            if (javaRuntimeFile.Type == "file")
            {
                if (javaRuntimeFile.Downloads == null)
                    return false;

                if (javaRuntimeFile.Downloads.Raw == null)
                    return false;

                if (string.IsNullOrEmpty(javaRuntimeFile.Downloads.Raw.URL))
                    return false;

                if (string.IsNullOrEmpty(javaRuntimeFile.Downloads.Raw.SHA1))
                    return false;

                if (
                    !await Request.Download(
                        Path.Combine(
                            MinecraftPathResolver.DownloadedJavaRuntimePath(
                                minecraftPath,
                                JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType)
                            ),
                            path
                        ),
                        javaRuntimeFile.Downloads.Raw.URL,
                        javaRuntimeFile.Downloads.Raw.SHA1
                    )
                )
                    return false;
            }
        }

        return true;
    }

    public static bool Exists(JavaRuntimeFiles javaRuntimeFiles)
    {
        if (javaRuntimeFiles == null)
            return false;

        if (javaRuntimeFiles.Files == null)
            return false;

        if (javaRuntimeFiles.Files.Count <= 0)
            return false;

        return true;
    }
}
