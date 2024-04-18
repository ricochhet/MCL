using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Java;
using MCL.Core.Services.Interfaces;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Java;

namespace MCL.Core.Services.Java;

public class JavaDownloadService : IDownloadService
{
    private static JavaRuntimeIndex JavaRuntimeIndex;
    private static JavaRuntimeFiles JavaRuntimeFiles;
    private static LauncherPath LauncherPath;
    private static MinecraftUrls MinecraftUrls;
    private static JavaRuntimeType JavaRuntimeType;
    private static JavaRuntimePlatform JavaRuntimePlatform;
    public static bool IsOffline { get; set; }

    public static void Init(
        LauncherPath launcherPath,
        MinecraftUrls minecraftUrls,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        LauncherPath = launcherPath;
        MinecraftUrls = minecraftUrls;
        JavaRuntimeType = javaRuntimeType;
        JavaRuntimePlatform = javaRuntimePlatform;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!useLocalVersionManifest && !await DownloadJavaRuntimeIndex())
            return false;

        if (!LoadJavaRuntimeIndex())
            return false;

        if (!await DownloadJavaRuntimeManifest())
            return false;

        if (!LoadJavaRuntimeManifest())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaRuntimeIndex()
    {
        if (!await JavaRuntimeIndexDownloader.Download(LauncherPath, MinecraftUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeIndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadJavaRuntimeIndex()
    {
        JavaRuntimeIndex = Json.Load<JavaRuntimeIndex>(JavaPathResolver.DownloadedJavaRuntimeIndexPath(LauncherPath));
        if (JavaRuntimeIndex == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(Models.Java.JavaRuntimeIndex)]);
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
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeManifestDownloader)]);
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
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(JavaRuntimeManifest)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(LauncherPath, JavaRuntimeType, JavaRuntimeFiles))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeDownloader)]);
            return false;
        }

        return true;
    }
}
