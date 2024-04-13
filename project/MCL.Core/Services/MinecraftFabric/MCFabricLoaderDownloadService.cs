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

public class MCFabricLoaderDownloadService : IFabricLoaderDownloadService, IDownloadService
{
    private static MCFabricIndex FabricIndex;
    private static MCFabricProfile FabricProfile;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCFabricConfigUrls FabricConfigUrls;
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

    public static bool LoadFabricIndex()
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

    public static async Task<bool> DownloadFabricProfile()
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

    public static bool LoadFabricProfile()
    {
        if (!Loaded)
            return false;

        FabricProfile = Json.Load<MCFabricProfile>(
            MinecraftFabricPathResolver.DownloadedFabricProfilePath(LauncherPath, LauncherVersion)
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

    public static bool LoadFabricLoaderVersion()
    {
        if (!Loaded)
            return false;

        MCFabricLoader fabricLoader = MCFabricVersionHelper.GetFabricLoaderVersion(LauncherVersion, FabricIndex);
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

    public static async Task<bool> DownloadFabricLoader()
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
