using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Web;

namespace MCL.Core.ModLoaders.Quilt.Services;

public class QuiltInstallerDownloadService : IJarDownloadService<QuiltUrls>, IDownloadService
{
    public static QuiltVersionManifest QuiltVersionManifest { get; private set; }
    public static QuiltInstaller QuiltInstaller { get; private set; }
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

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
            return false;

        return true;
    }

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await QuiltVersionManifestDownloader.Download(_launcherPath, _quiltUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", nameof(QuiltVersionManifestDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", nameof(QuiltVersionManifest));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        QuiltVersionManifest = Json.Load<QuiltVersionManifest>(QuiltPathResolver.VersionManifestPath(_launcherPath));
        if (ObjectValidator<QuiltVersionManifest>.IsNull(QuiltVersionManifest))
            return false;

        return true;
    }

    public static bool LoadVersion()
    {
        if (!_loaded)
            return false;

        QuiltInstaller = QuiltVersionHelper.GetInstallerVersion(_launcherVersion, QuiltVersionManifest);
        if (ObjectValidator<QuiltInstaller>.IsNull(QuiltInstaller))
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                _launcherVersion?.QuiltInstallerVersion,
                nameof(QuiltInstaller)
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!_loaded)
            return false;

        if (!await QuiltInstallerDownloader.Download(_launcherPath, _launcherVersion, QuiltInstaller))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", nameof(QuiltInstallerDownloader));
            return false;
        }

        return true;
    }
}
