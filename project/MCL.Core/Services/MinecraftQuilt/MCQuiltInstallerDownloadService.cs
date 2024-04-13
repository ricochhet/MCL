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

public class MCQuiltInstallerDownloadService : IQuiltInstallerDownloadService, IDownloadService
{
    private static MCQuiltIndex QuiltIndex;
    private static MCQuiltInstaller QuiltInstaller;
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

        if (!IsOffline && !UseExistingIndex && !await DownloadQuiltIndex())
            return false;

        if (!LoadQuiltIndex())
            return false;

        if (!LoadQuiltInstallerVersion())
            return false;

        if (!await DownloadQuiltInstaller())
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

    public static bool LoadQuiltInstallerVersion()
    {
        if (!Loaded)
            return false;

        QuiltInstaller = MCQuiltVersionHelper.GetQuiltInstallerVersion(LauncherVersion, QuiltIndex);
        if (QuiltInstaller == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [LauncherVersion?.QuiltInstallerVersion, nameof(MCQuiltInstaller)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadQuiltInstaller()
    {
        if (!Loaded)
            return false;

        if (!await QuiltInstallerDownloader.Download(LauncherPath, LauncherVersion, QuiltInstaller))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(QuiltInstallerDownloader)])
            );
            return false;
        }

        return true;
    }
}
