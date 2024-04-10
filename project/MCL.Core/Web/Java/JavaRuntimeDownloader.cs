using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Handlers.Java;
using MCL.Core.Interfaces.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Web.Java;

public class JavaRuntimeDownloader : IJavaRuntimeDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return false;

        if (!JavaRuntimeDownloaderErr.Exists(javaRuntimeFiles))
            return false;

        foreach ((string path, JavaRuntimeFile javaRuntimeFile) in javaRuntimeFiles.Files)
        {
            if (javaRuntimeFile.Downloads == null)
                continue;

            if (javaRuntimeFile.Type == "file")
            {
                if (!JavaRuntimeDownloaderErr.Exists(javaRuntimeFile))
                    return false;

                if (
                    !await Request.Download(
                        javaRuntimeFile.Downloads.Raw.URL,
                        VFS.Combine(
                            JavaPathResolver.DownloadedJavaRuntimePath(
                                launcherPath,
                                JavaRuntimeTypeResolver.ToString(javaRuntimeType)
                            ),
                            path
                        ),
                        javaRuntimeFile.Downloads.Raw.SHA1
                    )
                )
                    return false;
            }
        }

        return true;
    }
}
