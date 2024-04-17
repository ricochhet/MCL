using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Interfaces.Services.MinecraftFabric;
using MCL.Core.Interfaces.Web;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftQuilt;

namespace MCL.Core.Services.MinecraftQuilt;

public class QuiltLoaderDownloadService : IFabricLoaderDownloadService<MCQuiltConfigUrls>, IDownloadService
{
    public static MCQuiltIndex QuiltIndex { get; private set; }
    public static MCQuiltProfile QuiltProfile { get; private set; }
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCQuiltConfigUrls QuiltConfigUrls;
    public static bool UseExistingIndex { get; set; } = false;
    public static bool IsOffline { get; set; } = false;
    private static bool Loaded = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltConfigUrls quiltConfigUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        QuiltConfigUrls = quiltConfigUrls;
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

        if (!IsOffline && !await DownloadProfile())
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

        if (!await QuiltIndexDownloader.Download(LauncherPath, QuiltConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(QuiltIndexDownloader)]));
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        QuiltIndex = Json.Load<MCQuiltIndex>(QuiltPathResolver.DownloadedIndexPath(LauncherPath));
        if (QuiltIndex == null)
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.readfile", [nameof(MCQuiltIndex)]));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!Loaded)
            return false;

        if (!await QuiltProfileDownloader.Download(LauncherPath, LauncherVersion, QuiltConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(QuiltProfileDownloader)]));
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!Loaded)
            return false;

        if (!MCLauncherVersion.Exists(LauncherVersion))
            return false;

        QuiltProfile = Json.Load<MCQuiltProfile>(
            QuiltPathResolver.DownloadedProfilePath(LauncherPath, LauncherVersion)
        );
        if (QuiltProfile == null)
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(MCQuiltProfile)]));
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!Loaded)
            return false;

        MCQuiltLoader quiltLoader = QuiltVersionHelper.GetLoaderVersion(LauncherVersion, QuiltIndex);
        if (quiltLoader == null)
        {
            NotificationService.Add(
                new(NativeLogLevel.Error, "error.parse", [LauncherVersion?.QuiltLoaderVersion, nameof(MCQuiltLoader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!Loaded)
            return false;

        if (!await QuiltLoaderDownloader.Download(LauncherPath, LauncherVersion, QuiltProfile, QuiltConfigUrls))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(QuiltLoaderDownloader)]));
            return false;
        }

        return true;
    }
}
