using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltLoaderDownloadService : ILoaderDownloadService<QuiltUrls>, IDownloadService
{
    public static QuiltIndex QuiltIndex { get; private set; }
    public static QuiltProfile QuiltProfile { get; private set; }
    private static LauncherPath LauncherPath;
    private static LauncherVersion LauncherVersion;
    private static QuiltUrls QuiltUrls;
    private static bool Loaded = false;

    public static void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, QuiltUrls quiltUrls)
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        QuiltUrls = quiltUrls;
        Loaded = true;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!Loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadIndex())
            return false;

        if (!LoadIndex())
            return false;

        if (!await DownloadProfile())
            return false;

        if (!LoadProfile())
            return false;

        if (!LoadLoaderVersion())
            return false;

        if (!await DownloadLoader())
            return false;

        return true;
    }

    public static async Task<bool> DownloadIndex()
    {
        if (!Loaded)
            return false;

        if (!await QuiltIndexDownloader.Download(LauncherPath, QuiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltIndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        QuiltIndex = Json.Load<QuiltIndex>(QuiltPathResolver.DownloadedIndexPath(LauncherPath));
        if (QuiltIndex == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(QuiltIndex)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!Loaded)
            return false;

        if (!await QuiltProfileDownloader.Download(LauncherPath, LauncherVersion, QuiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltProfileDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!Loaded)
            return false;

        if (!LauncherVersion.VersionsExists())
            return false;

        QuiltProfile = Json.Load<QuiltProfile>(QuiltPathResolver.DownloadedProfilePath(LauncherPath, LauncherVersion));
        if (QuiltProfile == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltProfile)]);
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!Loaded)
            return false;

        QuiltLoader quiltLoader = QuiltVersionHelper.GetLoaderVersion(LauncherVersion, QuiltIndex);
        if (quiltLoader == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.QuiltLoaderVersion, nameof(QuiltLoader)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!Loaded)
            return false;

        if (!await QuiltLoaderDownloader.Download(LauncherPath, LauncherVersion, QuiltProfile, QuiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltLoaderDownloader)]);
            return false;
        }

        return true;
    }
}
