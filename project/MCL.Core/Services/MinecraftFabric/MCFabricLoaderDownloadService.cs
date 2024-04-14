using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Interfaces;
using MCL.Core.Interfaces.Services.MinecraftFabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftFabric;

namespace MCL.Core.Services.MinecraftFabric;

public class MCFabricLoaderDownloadService : IFabricLoaderDownloadService<MCFabricConfigUrls>, IDownloadService
{
    private static MCFabricIndex FabricIndex;
    private static MCFabricProfile FabricProfile;
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

        FabricIndex = Json.Load<MCFabricIndex>(MinecraftFabricPathResolver.DownloadedIndexPath(LauncherPath));
        if (FabricIndex == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!Loaded)
            return false;

        if (!await FabricProfileDownloader.Download(LauncherPath, LauncherVersion, FabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricProfileDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!Loaded)
            return false;

        FabricProfile = Json.Load<MCFabricProfile>(
            MinecraftFabricPathResolver.DownloadedProfilePath(LauncherPath, LauncherVersion)
        );
        if (FabricProfile == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(MCFabricProfile)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!Loaded)
            return false;

        MCFabricLoader fabricLoader = MCFabricVersionHelper.GetLoaderVersion(LauncherVersion, FabricIndex);
        if (fabricLoader == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.parse",
                    [LauncherVersion?.FabricLoaderVersion, nameof(MCFabricLoader)]
                )
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!Loaded)
            return false;

        if (!await FabricLoaderDownloader.Download(LauncherPath, LauncherVersion, FabricProfile, FabricConfigUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(FabricLoaderDownloader)])
            );
            return false;
        }

        return true;
    }
}
