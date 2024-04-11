using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Java;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services;
using MCL.Core.Web.Java;

namespace MCL.Core.Providers.Java;

public class JavaDownloadProvider(
    MCLauncherPath _launcherPath,
    MCConfigUrls _configUrls,
    JavaRuntimeType _javaRuntimeType,
    JavaRuntimePlatform _javaRuntimePlatform
)
{
    private JavaRuntimeIndex javaRuntimeIndex;
    private JavaRuntimeFiles javaRuntimeFiles;
    private readonly MCLauncherPath launcherPath = _launcherPath;
    private readonly MCConfigUrls configUrls = _configUrls;
    private readonly JavaRuntimeType javaRuntimeType = _javaRuntimeType;
    private readonly JavaRuntimePlatform javaRuntimePlatform = _javaRuntimePlatform;

    public async Task<bool> DownloadAll()
    {
        if (!await DownloadJavaRuntimeIndex())
            return false;

        if (!await DownloadJavaRuntimeManifest())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public async Task<bool> DownloadJavaRuntimeIndex()
    {
        if (!await JavaRuntimeIndexDownloader.Download(launcherPath, configUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeIndexDownloader)])
            );
            return false;
        }

        javaRuntimeIndex = Json.Load<JavaRuntimeIndex>(JavaPathResolver.DownloadedJavaRuntimeIndexPath(launcherPath));
        if (javaRuntimeIndex == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.readfile", [nameof(JavaRuntimeIndex)])
            );
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadJavaRuntimeManifest()
    {
        if (
            !await JavaRuntimeManifestDownloader.Download(
                launcherPath,
                javaRuntimePlatform,
                javaRuntimeType,
                javaRuntimeIndex
            )
        )
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeManifestDownloader)])
            );
            return false;
        }

        javaRuntimeFiles = Json.Load<JavaRuntimeFiles>(
            JavaPathResolver.DownloadedJavaRuntimeManifestPath(
                launcherPath,
                JavaRuntimeTypeResolver.ToString(javaRuntimeType)
            )
        );
        if (javaRuntimeFiles == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.readfile", [nameof(JavaRuntimeManifest)])
            );
            return false;
        }

        return true;
    }

    public async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(launcherPath, javaRuntimeType, javaRuntimeFiles))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeDownloader)])
            );
            return false;
        }

        return true;
    }
}
