using System.Threading.Tasks;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services.Interfaces;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Services.Minecraft;

public class MinecraftDownloadService : IDownloadService
{
    public static MinecraftVersionManifest VersionManifest { get; private set; }
    public static MinecraftVersionDetails VersionDetails { get; private set; }
    public static MinecraftVersion Version { get; private set; }
    private static MinecraftAssetsData Assets;
    private static LauncherPath LauncherPath;
    private static LauncherVersion LauncherVersion;
    private static LauncherSettings LauncherSettings;
    private static MinecraftUrls MinecraftUrls;
    private static bool Loaded = false;

    public static void Init(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherSettings launcherSettings,
        MinecraftUrls minecraftUrls
    )
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        LauncherSettings = launcherSettings;
        MinecraftUrls = minecraftUrls;
        Loaded = true;
    }

#pragma warning disable IDE0079
#pragma warning disable S3776
    public static async Task<bool> Download(bool useLocalVersionManifest = false)
#pragma warning restore
    {
        if (!Loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadVersionDetails())
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

        if (!await DownloadAssetIndex())
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

        if (!await VersionManifestDownloader.Download(LauncherPath, MinecraftUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(MinecraftVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!Loaded)
            return false;

        VersionManifest = Json.Load<MinecraftVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(LauncherPath)
        );
        if (VersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MinecraftVersionManifest)]);
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
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.Version, nameof(MinecraftVersion)]
            );
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

        VersionDetails = Json.Load<MinecraftVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(LauncherPath, Version)
        );
        if (VersionDetails == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MinecraftVersionDetails)]);
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

        Assets = Json.Load<MinecraftAssetsData>(MinecraftPathResolver.ClientIndexPath(LauncherPath, VersionDetails));
        if (Assets == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MinecraftAssetsData)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadResources()
    {
        if (!Loaded)
            return false;

        if (!await ResourceDownloader.Download(LauncherPath, MinecraftUrls, Assets))
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
