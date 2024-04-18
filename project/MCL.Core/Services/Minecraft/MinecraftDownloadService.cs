using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.Services.Minecraft;
using MCL.Core.Interfaces.Web;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Services.Minecraft;

public class MinecraftDownloadService : IMinecraftDownloadService, IDownloadService
{
    public static MCVersionManifest VersionManifest { get; private set; }
    public static MCVersionDetails VersionDetails { get; private set; }
    public static MCVersion Version { get; private set; }
    private static MCAssetsData Assets;
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCLauncherSettings LauncherSettings;
    private static MCConfigUrls ConfigUrls;
    public static bool UseExistingIndex { get; set; } = false;
    public static bool IsOffline { get; set; } = false;
    private static bool Loaded = false;

    public static void Init(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCLauncherSettings launcherSettings,
        MCConfigUrls configUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        LauncherSettings = launcherSettings;
        ConfigUrls = configUrls;
        Loaded = true;
    }

#pragma warning disable IDE0079
#pragma warning disable S3776
    public static async Task<bool> Download()
#pragma warning restore
    {
        if (!Loaded)
            return false;

        if (!IsOffline && !UseExistingIndex && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest(false))
            return false;

        if (!LoadVersion())
            return false;

        if (!IsOffline && !UseExistingIndex && !await DownloadVersionDetails())
            return false;

        if (!LoadVersionDetails())
            return false;

        if (!await DownloadLibraries())
            return false;

        if (!await DownloadClient())
            return false;

        if (!await DownloadClientMappings())
            return false;

        if (!await DownloadServer())
            return false;

        if (!await DownloadServerMappings())
            return false;

        if (!IsOffline && !await DownloadAssetIndex())
            return false;

        if (!LoadAssetIndex())
            return false;

        if (!await DownloadResources())
            return false;

        if (!await DownloadLogging())
            return false;

        return true;
    }

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!Loaded)
            return false;

        if (!await VersionManifestDownloader.Download(LauncherPath, ConfigUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(MCVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest(bool silent)
    {
        if (!Loaded)
            return false;

        VersionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(LauncherPath)
        );
        if (VersionManifest == null)
        {
            if (!silent)
                NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MCVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersion()
    {
        if (!Loaded)
            return false;

        Version = VersionHelper.GetVersion(LauncherVersion, VersionManifest);
        if (Version == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.parse", [LauncherVersion?.Version, nameof(MCVersion)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadVersionDetails()
    {
        if (!Loaded)
            return false;

        if (!await VersionDetailsDownloader.Download(LauncherPath, Version))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(VersionDetailsDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionDetails()
    {
        if (!Loaded)
            return false;

        VersionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(LauncherPath, Version)
        );
        if (VersionDetails == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MCVersionDetails)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLibraries()
    {
        if (!Loaded)
            return false;

        if (!await LibraryDownloader.Download(LauncherPath, LauncherSettings, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(LibraryDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClient()
    {
        if (!Loaded)
            return false;

        if (!await ClientDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ClientDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClientMappings()
    {
        if (!Loaded)
            return false;

        if (!await ClientMappingsDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ClientMappingsDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServer()
    {
        if (!Loaded)
            return false;

        if (!await ServerDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ServerDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServerMappings()
    {
        if (!Loaded)
            return false;

        if (!await ServerMappingsDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ServerMappingsDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadAssetIndex()
    {
        if (!Loaded)
            return false;

        if (!await IndexDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(IndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadAssetIndex()
    {
        if (!Loaded)
            return false;

        Assets = Json.Load<MCAssetsData>(MinecraftPathResolver.ClientIndexPath(LauncherPath, VersionDetails));
        if (Assets == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MCAssetsData)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadResources()
    {
        if (!Loaded)
            return false;

        if (!await ResourceDownloader.Download(LauncherPath, ConfigUrls, Assets))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ResourceDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLogging()
    {
        if (!Loaded)
            return false;

        if (!await LoggingDownloader.Download(LauncherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(LoggingDownloader)]);
            return false;
        }

        return true;
    }
}
