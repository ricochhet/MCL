using System.Threading.Tasks;
using MCL.Core.Helpers.ModLoaders.Quilt;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Resolvers.ModLoaders.Quilt;
using MCL.Core.Services.Interfaces;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.ModLoaders.Quilt;

namespace MCL.Core.Services.ModLoaders.Quilt;

public class QuiltInstallerDownloadService : IJarDownloadService<QuiltUrls>, IDownloadService
{
    public static QuiltIndex QuiltIndex { get; private set; }
    public static QuiltInstaller QuiltInstaller { get; private set; }
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

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
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
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.readfile",
                [nameof(Models.ModLoaders.Quilt.QuiltIndex)]
            );
            return false;
        }

        return true;
    }

    public static bool LoadVersion()
    {
        if (!Loaded)
            return false;

        QuiltInstaller = QuiltVersionHelper.GetInstallerVersion(LauncherVersion, QuiltIndex);
        if (QuiltInstaller == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.QuiltInstallerVersion, nameof(Models.ModLoaders.Quilt.QuiltInstaller)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!Loaded)
            return false;

        if (!await QuiltInstallerDownloader.Download(LauncherPath, LauncherVersion, QuiltInstaller))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(QuiltInstallerDownloader)]);
            return false;
        }

        return true;
    }
}
