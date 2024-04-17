using System.Threading.Tasks;
using MCL.Core.Helpers.Paper;
using MCL.Core.Interfaces.Services.Paper;
using MCL.Core.Interfaces.Web;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Paper;
using MCL.Core.Resolvers.Paper;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Paper;

namespace MCL.Core.Services.Paper;

public class PaperServerDownloadService : IPaperServerDownloadService<PaperConfigUrls>, IDownloadService
{
    public static PaperVersionManifest PaperVersionManifest { get; private set; }
    public static PaperBuild PaperBuild { get; private set; }
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static PaperConfigUrls PaperConfigUrls;
    public static bool UseExistingIndex { get; set; } = false;
    public static bool IsOffline { get; set; } = false;
    private static bool Loaded = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        PaperConfigUrls paperConfigUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        PaperConfigUrls = paperConfigUrls;
        Loaded = true;
    }

    public static async Task<bool> Download()
    {
        if (!Loaded)
            return false;

        if (!IsOffline && !UseExistingIndex && !await DownloadIndex())
            return false;

        if (!LoadIndex())
            return false;

        if (!LoadServerVersion())
            return false;

        if (!await DownloadServer())
            return false;

        return true;
    }

    public static async Task<bool> DownloadIndex()
    {
        if (!Loaded)
            return false;

        if (!await PaperIndexDownloader.Download(LauncherPath, LauncherVersion, PaperConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(PaperIndexDownloader)]));
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        if (!MCLauncherVersion.Exists(LauncherVersion))
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.DownloadedIndexPath(LauncherPath, LauncherVersion)
        );
        if (PaperVersionManifest == null)
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.readfile", [nameof(MCQuiltIndex)]));
            return false;
        }

        return true;
    }

    public static bool LoadServerVersion()
    {
        if (!Loaded)
            return false;

        PaperBuild = PaperVersionHelper.GetVersion(LauncherVersion, PaperVersionManifest);
        if (PaperBuild == null)
        {
            NotificationService.Add(
                new(NativeLogLevel.Error, "error.parse", [LauncherVersion?.FabricInstallerVersion, nameof(PaperBuild)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServer()
    {
        if (!Loaded)
            return false;

        if (!await PaperServerDownloader.Download(LauncherPath, LauncherVersion, PaperBuild, PaperConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(PaperServerDownloader)]));
            return false;
        }

        return true;
    }
}
