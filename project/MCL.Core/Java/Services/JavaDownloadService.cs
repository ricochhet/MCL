using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Java.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Services;

public class JavaDownloadService : IDownloadService
{
    private static JavaVersionManifest _javaVersionManifest;
    private static JavaVersionDetails _javaVersionDetails;
    private static LauncherPath _launcherPath;
    private static MUrls _mUrls;
    private static JavaRuntimeType _javaRuntimeType;
    private static JavaRuntimePlatform _javaRuntimePlatform;

    public static void Init(
        LauncherPath launcherPath,
        MUrls mUrls,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        _launcherPath = launcherPath;
        _mUrls = mUrls;
        _javaRuntimeType = javaRuntimeType;
        _javaRuntimePlatform = javaRuntimePlatform;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!useLocalVersionManifest && !await DownloadJavaVersionManifest())
            return false;

        if (!LoadJavaVersionManifest())
            return false;

        if (!await DownloadJavaVersionDetails())
            return false;

        if (!LoadJavaVersionDetails())
            return false;

        if (!await DownloadJavaRuntime())
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaVersionManifest()
    {
        if (!await JavaVersionManifestDownloader.Download(_launcherPath, _mUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaVersionManifestDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionManifest()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.DownloadedJavaVersionManifestPath(_launcherPath)
        );
        if (_javaVersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(_javaVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionManifestWithoutLogging()
    {
        _javaVersionManifest = Json.Load<JavaVersionManifest>(
            JavaPathResolver.DownloadedJavaVersionManifestPath(_launcherPath)
        );
        if (_javaVersionManifest == null)
            return false;

        return true;
    }

    public static async Task<bool> DownloadJavaVersionDetails()
    {
        if (
            !await JavaVersionDetailsDownloader.Download(
                _launcherPath,
                _javaRuntimePlatform,
                _javaRuntimeType,
                _javaVersionManifest
            )
        )
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaVersionDetailsDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadJavaVersionDetails()
    {
        _javaVersionDetails = Json.Load<JavaVersionDetails>(
            JavaPathResolver.DownloadedJavaVersionDetailsPath(
                _launcherPath,
                JavaRuntimeTypeResolver.ToString(_javaRuntimeType)
            )
        );
        if (_javaVersionDetails == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(_javaVersionDetails)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJavaRuntime()
    {
        if (!await JavaRuntimeDownloader.Download(_launcherPath, _javaRuntimeType, _javaVersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(JavaRuntimeDownloader)]);
            return false;
        }

        return true;
    }
}
