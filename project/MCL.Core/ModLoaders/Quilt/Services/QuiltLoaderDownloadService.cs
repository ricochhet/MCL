using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltLoaderDownloadService : ILoaderDownloadService<QuiltUrls>, IDownloadService
{
    public static QuiltVersionManifest QuiltVersionManifest { get; private set; }
    public static QuiltProfile QuiltProfile { get; private set; }
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static QuiltUrls _quiltUrls;
    private static bool _loaded = false;

    public static void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, QuiltUrls quiltUrls)
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _quiltUrls = quiltUrls;
        _loaded = true;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!_loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
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

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await QuiltVersionManifestDownloader.Download(_launcherPath, _quiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltVersionManifestDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (QuiltVersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(QuiltVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (QuiltVersionManifest == null)
            return false;

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!_loaded)
            return false;

        if (!await QuiltProfileDownloader.Download(_launcherPath, _launcherVersion, _quiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltProfileDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!_loaded)
            return false;

        if (!_launcherVersion.VersionExists() || !_launcherVersion.QuiltLoaderVersionExists())
            return false;

        QuiltProfile = Json.Load<QuiltProfile>(QuiltPathResolver.ProfilePath(_launcherPath, _launcherVersion));
        if (QuiltProfile == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltProfile)]);
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!_loaded)
            return false;

        QuiltLoader quiltLoader = QuiltVersionHelper.GetLoaderVersion(_launcherVersion, QuiltVersionManifest);
        if (quiltLoader == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [_launcherVersion?.QuiltLoaderVersion, nameof(QuiltLoader)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!_loaded)
            return false;

        if (!await QuiltLoaderDownloader.Download(_launcherPath, _launcherVersion, QuiltProfile, _quiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltLoaderDownloader)]);
            return false;
        }

        return true;
    }
}
