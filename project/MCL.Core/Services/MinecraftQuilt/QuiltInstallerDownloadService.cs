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

public class QuiltInstallerDownloadService : IFabricInstallerDownloadService<MCQuiltConfigUrls>, IDownloadService
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

        if (!IsOffline && !UseExistingIndex && !await DownloadIndex())
            return false;

        if (!LoadIndex())
            return false;

        if (!LoadInstallerVersion())
            return false;

        if (!await DownloadInstaller())
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

    public static bool LoadInstallerVersion()
    {
        if (!Loaded)
            return false;

        QuiltInstaller = QuiltVersionHelper.GetInstallerVersion(LauncherVersion, QuiltIndex);
        if (QuiltInstaller == null)
        {
            NotificationService.Add(
                new(
                    NativeLogLevel.Error,
                    "error.parse",
                    [LauncherVersion?.QuiltInstallerVersion, nameof(MCQuiltInstaller)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadInstaller()
    {
        if (!Loaded)
            return false;

        if (!await QuiltInstallerDownloader.Download(LauncherPath, LauncherVersion, QuiltInstaller))
        {
            NotificationService.Add(new(NativeLogLevel.Error, "error.download", [nameof(QuiltInstallerDownloader)]));
            return false;
        }

        return true;
    }
}
