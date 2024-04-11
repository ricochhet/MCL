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
using MCL.Core.Services;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Providers.MinecraftFabric;

public class MCFabricInstallerDownloadService : IFabricInstallerDownloadService, IDownloadService
{
    private static MCFabricIndex fabricIndex;
    private static MCFabricInstaller fabricInstaller;
    private static MCLauncherPath launcherPath;
    private static MCLauncherVersion launcherVersion;
    private static MCFabricConfigUrls fabricConfigUrls;
    private static bool IsOffline { get; set; } = false;

    public static void Init(
        MCLauncherPath _launcherPath,
        MCLauncherVersion _launcherVersion,
        MCFabricConfigUrls _fabricConfigUrls
    )
    {
        launcherPath = _launcherPath;
        launcherVersion = _launcherVersion;
        fabricConfigUrls = _fabricConfigUrls;
    }

    public static async Task<bool> Download()
    {
        if (!await DownloadFabricIndex() && !IsOffline)
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
        if (!await FabricIndexDownloader.Download(launcherPath, fabricConfigUrls))
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
        fabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedFabricIndexPath(launcherPath));
        if (fabricIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]));
            return false;
        }

        return true;
    }

    public static bool LoadFabricInstallerVersion()
    {
        fabricInstaller = MCFabricVersionHelper.GetFabricInstallerVersion(launcherVersion, fabricIndex);
        if (fabricInstaller == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [launcherVersion?.FabricInstallerVersion, nameof(MCFabricInstaller)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadFabricInstaller()
    {
        if (!await FabricInstallerDownloader.Download(launcherPath, fabricInstaller))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricInstallerDownloader)])
            );
            return false;
        }

        return true;
    }
}
