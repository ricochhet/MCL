using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Extensions.Java;
using MCL.Core.Interfaces.Web.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Web.Java;

public class JavaRuntimeDownloader : IJavaRuntimeDownloader
{
    public static async Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
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
