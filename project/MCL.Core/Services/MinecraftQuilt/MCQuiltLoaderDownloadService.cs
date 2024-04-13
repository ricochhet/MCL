using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Interfaces.MinecraftQuilt;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftQuilt;

namespace MCL.Core.Providers.MinecraftQuilt;

public class MCQuiltLoaderDownloadService : IQuiltLoaderDownloadService, IDownloadService
{
    private static MCQuiltIndex QuiltIndex;
    private static MCQuiltProfile QuiltProfile;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCQuiltConfigUrls QuiltConfigUrls;
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

        if (!IsOffline && !await DownloadQuiltIndex())
            return false;

        if (!LoadQuiltIndex())
            return false;

        if (!IsOffline && !await DownloadQuiltProfile())
            return false;

        if (!LoadQuiltProfile())
            return false;

        if (!LoadQuiltLoaderVersion())
            return false;

        if (!await DownloadQuiltLoader())
            return false;

        return true;
    }

    public static async Task<bool> DownloadQuiltIndex()
    {
        if (!Loaded)
            return false;

        if (!await QuiltIndexDownloader.Download(LauncherPath, QuiltConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(QuiltIndexDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadQuiltIndex()
    {
        if (!Loaded)
            return false;

        QuiltIndex = Json.Load<MCQuiltIndex>(MinecraftQuiltPathResolver.DownloadedQuiltIndexPath(LauncherPath));
        if (QuiltIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCQuiltIndex)]));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadQuiltProfile()
    {
        if (!Loaded)
            return false;

        if (!await QuiltProfileDownloader.Download(LauncherPath, LauncherVersion, QuiltConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(QuiltProfileDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadQuiltProfile()
    {
        if (!Loaded)
            return false;

        QuiltProfile = Json.Load<MCQuiltProfile>(
            MinecraftQuiltPathResolver.DownloadedQuiltProfilePath(LauncherPath, LauncherVersion)
        );
        if (QuiltProfile == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.download", [nameof(MCQuiltProfile)]));
            return false;
        }

        return true;
    }

    public static bool LoadQuiltLoaderVersion()
    {
        if (!Loaded)
            return false;

        MCQuiltLoader quiltLoader = MCQuiltVersionHelper.GetQuiltLoaderVersion(LauncherVersion, QuiltIndex);
        if (quiltLoader == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [LauncherVersion?.QuiltLoaderVersion, nameof(MCQuiltLoader)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadQuiltLoader()
    {
        if (!Loaded)
            return false;

        if (!await QuiltLoaderDownloader.Download(LauncherPath, LauncherVersion, QuiltProfile, QuiltConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(QuiltLoaderDownloader)])
            );
            return false;
        }

        return true;
    }
}
