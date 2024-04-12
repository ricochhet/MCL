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

public class MCFabricLoaderDownloadService : IFabricLoaderDownloadService, IDownloadService
{
    private static MCFabricIndex fabricIndex;
    private static MCFabricProfile fabricProfile;
    private static MCLauncherPath launcherPath;
    private static MCLauncherVersion launcherVersion;
    private static MCFabricConfigUrls fabricConfigUrls;
    public static bool IsOffline { get; set; } = false;

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
        if (!IsOffline && !await DownloadFabricIndex())
            return false;

        if (!LoadFabricIndex())
            return false;

        if (!IsOffline && !await DownloadFabricProfile())
            return false;

        if (!LoadFabricProfile())
            return false;

        if (!LoadFabricLoaderVersion())
            return false;

        if (!await DownloadFabricLoader())
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

    public static async Task<bool> DownloadFabricProfile()
    {
        if (!await FabricProfileDownloader.Download(launcherPath, launcherVersion, fabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricProfileDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadFabricProfile()
    {
        fabricProfile = Json.Load<MCFabricProfile>(
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(launcherPath, launcherVersion)
        );
        if (fabricProfile == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(MCFabricProfile)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadFabricLoaderVersion()
    {
        MCFabricLoader fabricLoader = MCFabricVersionHelper.GetFabricLoaderVersion(launcherVersion, fabricIndex);
        if (fabricLoader == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [launcherVersion?.FabricLoaderVersion, nameof(MCFabricLoader)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadFabricLoader()
    {
        if (!await FabricLoaderDownloader.Download(launcherPath, launcherVersion, fabricProfile, fabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricLoaderDownloader)])
            );
            return false;
        }

        return true;
    }
}
