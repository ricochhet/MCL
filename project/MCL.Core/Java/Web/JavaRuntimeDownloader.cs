using System.Threading.Tasks;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Web;

public static class JavaRuntimeDownloader
{
    public static async Task<bool> Download(
        LauncherPath launcherPath,
        JavaRuntimeType javaRuntimeType,
        JavaVersionDetails javaRuntimeFiles
    )
    {
        if (!javaRuntimeFiles.FilesExists())
            return false;

        foreach ((string path, JavaRuntimeFile javaRuntimeFile) in javaRuntimeFiles.Files)
        {
            if (javaRuntimeFile.Downloads == null)
                continue;

            if (javaRuntimeFile.Type == "file")
            {
                if (!javaRuntimeFile.FileExists())
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
