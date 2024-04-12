using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Interfaces.Java;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricInstallerDownloadService : IFabricInstallerDownloadService, IDownloadService
{
    private static MCFabricIndex FabricIndex;
    private static MCFabricInstaller FabricInstaller;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCFabricConfigUrls FabricConfigUrls;
    public static bool IsOffline { get; set; } = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCFabricConfigUrls fabricConfigUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        FabricConfigUrls = fabricConfigUrls;
    }

    public static async Task<bool> Download()
    {
        if (!IsOffline && !await DownloadFabricIndex())
            return false;

        if (!LoadFabricIndex())
            return false;

        if (!LoadFabricInstallerVersion())
            return false;

        if (!await DownloadFabricInstaller())
            return false;

        return true;
    }

    public static async Task<bool> DownloadFabricIndex()
    {
        if (!await FabricIndexDownloader.Download(LauncherPath, FabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricIndexDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadFabricIndex()
    {
        FabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(LauncherPath));
        if (FabricIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]));
            return false;
        }

        return true;
    }

    public static bool LoadFabricInstallerVersion()
    {
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

    public static async Task<bool> DownloadFabricInstaller()
    {
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
