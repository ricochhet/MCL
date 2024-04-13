using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Interfaces.MinecraftFabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftFabric;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricInstallerDownloadService : IFabricInstallerDownloadService<MCFabricConfigUrls>, IDownloadService
{
    private static MCFabricIndex FabricIndex;
    private static MCFabricInstaller FabricInstaller;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCFabricConfigUrls FabricConfigUrls;
    public static bool UseExistingIndex { get; set; } = false;
    public static bool IsOffline { get; set; } = false;
    private static bool Loaded = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        FabricConfigUrls = fabricConfigUrls;
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

        if (!await FabricIndexDownloader.Download(LauncherPath, FabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricIndexDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        FabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(LauncherPath));
        if (FabricIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]));
            return false;
        }

        return true;
    }

    public static bool LoadInstallerVersion()
    {
        if (!Loaded)
            return false;

        FabricInstaller = MCFabricVersionHelper.GetFabricInstallerVersion(LauncherVersion, FabricIndex);
        if (FabricInstaller == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [LauncherVersion?.FabricInstallerVersion, nameof(MCFabricInstaller)]
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

        if (!await FabricInstallerDownloader.Download(LauncherPath, LauncherVersion, FabricInstaller))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricInstallerDownloader)])
            );
            return false;
        }

        return true;
    }
}
