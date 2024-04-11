using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Interfaces.Java;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Java;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Java;

namespace MCL.Core.Services.Java;

public class JavaDownloadService : IJavaDownloadService, IDownloadService
{
    private static JavaRuntimeIndex javaRuntimeIndex;
    private static JavaRuntimeFiles javaRuntimeFiles;
    private static MCLauncherPath launcherPath;
    private static MCConfigUrls configUrls;
    private static JavaRuntimeType javaRuntimeType;
    private static JavaRuntimePlatform javaRuntimePlatform;
    public static bool IsOffline { get; set; }

    public static void Init(
        MCLauncherPath _launcherPath,
        MCConfigUrls _configUrls,
        JavaRuntimeType _javaRuntimeType,
        JavaRuntimePlatform _javaRuntimePlatform
    )
    {
        launcherPath = _launcherPath;
        configUrls = _configUrls;
        javaRuntimeType = _javaRuntimeType;
        javaRuntimePlatform = _javaRuntimePlatform;
    }

    public static async Task<bool> Download()
    {
        if (!await DownloadJavaRuntimeIndex() && !IsOffline)
            return false;

        if (!LoadJavaRuntimeIndex())
            return false;

        if (!await DownloadJavaRuntimeManifest() && !IsOffline)
            return false;

        if (!LoadJavaRuntimeManifest())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaRuntimeIndex()
    {
        if (!await JavaRuntimeIndexDownloader.Download(launcherPath, configUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeIndexDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadJavaRuntimeIndex()
    {
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

    public static async Task<bool> DownloadJavaRuntimeManifest()
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

        return true;
    }

    public static bool LoadJavaRuntimeManifest()
    {
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

    public static async Task<bool> DownloadJavaRuntime()
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
