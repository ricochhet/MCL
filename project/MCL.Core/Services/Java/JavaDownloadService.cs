using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Interfaces.Services.Java;
using MCL.Core.Interfaces.Web;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Java;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Java;

namespace MCL.Core.Services.Java;

public class JavaDownloadService : IJavaDownloadService, IDownloadService
{
    private static JavaRuntimeIndex JavaRuntimeIndex;
    private static JavaRuntimeFiles JavaRuntimeFiles;
    private static MCLauncherPath LauncherPath;
    private static MCConfigUrls ConfigUrls;
    private static JavaRuntimeType JavaRuntimeType;
    private static JavaRuntimePlatform JavaRuntimePlatform;
    public static bool IsOffline { get; set; }

    public static void Init(
        MCLauncherPath launcherPath,
        MCConfigUrls configUrls,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        LauncherPath = launcherPath;
        ConfigUrls = configUrls;
        JavaRuntimeType = javaRuntimeType;
        JavaRuntimePlatform = javaRuntimePlatform;
    }

    public static async Task<bool> Download()
    {
        if (!IsOffline && !await DownloadJavaRuntimeIndex())
            return false;

        if (!LoadJavaRuntimeIndex())
            return false;

        if (!IsOffline && !await DownloadJavaRuntimeManifest())
            return false;

        if (!LoadJavaRuntimeManifest())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaRuntimeIndex()
    {
        if (!await JavaRuntimeIndexDownloader.Download(LauncherPath, ConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeIndexDownloader)]));
            return false;
        }

        return true;
    }

    public static bool LoadJavaRuntimeIndex()
    {
        JavaRuntimeIndex = Json.Load<JavaRuntimeIndex>(JavaPathResolver.DownloadedJavaRuntimeIndexPath(LauncherPath));
        if (JavaRuntimeIndex == null)
        {
            NotificationService.Add(
                new(NativeLogLevel.Error, "error.readfile", [nameof(Models.Java.JavaRuntimeIndex)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJavaRuntimeManifest()
    {
        if (
            !await JavaRuntimeManifestDownloader.Download(
                LauncherPath,
                JavaRuntimePlatform,
                JavaRuntimeType,
                JavaRuntimeIndex
            )
        )
        {
            NotificationService.Add(
                new(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeManifestDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadJavaRuntimeManifest()
    {
        JavaRuntimeFiles = Json.Load<JavaRuntimeFiles>(
            JavaPathResolver.DownloadedJavaRuntimeManifestPath(
                LauncherPath,
                JavaRuntimeTypeResolver.ToString(JavaRuntimeType)
            )
        );
        if (JavaRuntimeFiles == null)
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.readfile", [nameof(JavaRuntimeManifest)]));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(LauncherPath, JavaRuntimeType, JavaRuntimeFiles))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeDownloader)]));
            return false;
        }

        return true;
    }
}
