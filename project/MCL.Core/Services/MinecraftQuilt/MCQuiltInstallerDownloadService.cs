using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Interfaces.Java;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftQuilt;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftQuilt;

public class MCQuiltInstallerDownloadService : IQuiltInstallerDownloadService, IDownloadService
{
    private static MCQuiltIndex QuiltIndex;
    private static MCQuiltInstaller QuiltInstaller;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCQuiltConfigUrls QuiltConfigUrls;
    public static bool IsOffline { get; set; } = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCQuiltConfigUrls quiltConfigUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        QuiltConfigUrls = quiltConfigUrls;
    }

    public static async Task<bool> Download()
    {
        if (!IsOffline && !await DownloadQuiltIndex())
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
