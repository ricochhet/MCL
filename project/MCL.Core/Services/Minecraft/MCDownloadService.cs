using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.Java;
using MCL.Core.Interfaces.Minecraft;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.Minecraft;

namespace MCL.Core.Services.Minecraft;

public class MCDownloadService : IMCDownloadService, IDownloadService
{
    private static MCVersionDetails versionDetails;
    private static MCVersionManifest versionManifest;
    private static MCVersion version;
    private static MCAssetsData assets;
    private static MCLauncherPath launcherPath;
    private static MCLauncherVersion launcherVersion;
    private static Platform platform;
    private static MCConfigUrls configUrls;
    private static bool IsOffline { get; set; } = false;

    public static void Init(
        MCLauncherPath _launcherPath,
        MCLauncherVersion _launcherVersion,
        Platform _platform,
        MCConfigUrls _configUrls
    )
    {
        launcherPath = _launcherPath;
        launcherVersion = _launcherVersion;
        platform = _platform;
        configUrls = _configUrls;
    }

#pragma warning disable IDE0079
#pragma warning disable S3776 // (Reduce cognitive complexity) TODO: Evaluate refactor
    public static async Task<bool> Download()
#pragma warning restore
    {
        if (!await DownloadVersionManifest() && !IsOffline)
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!LoadVersion())
            return false;

        if (!await DownloadVersionDetails() && !IsOffline)
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

        if (!await DownloadAssetIndex() && !IsOffline)
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
        if (!await VersionManifestDownloader.Download(launcherPath, configUrls))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(MCVersionManifest)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );
        if (versionManifest == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCVersionManifest)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadVersion()
    {
        version = MCVersionHelper.GetVersion(launcherVersion, versionManifest);
        if (version == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.parse", [launcherVersion?.Version, nameof(MCVersion)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadVersionDetails()
    {
        if (!await VersionDetailsDownloader.Download(launcherPath, version))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(VersionDetailsDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadVersionDetails()
    {
        versionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );
        if (versionDetails == null)
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCVersionDetails)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLibraries()
    {
        if (!await LibraryDownloader.Download(launcherPath, platform, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(LibraryDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClient()
    {
        if (!await ClientDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(ClientDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClientMappings()
    {
        if (!await ClientMappingsDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(ClientMappingsDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServer()
    {
        if (!await ServerDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(ServerDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServerMappings()
    {
        if (!await ServerMappingsDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(ServerMappingsDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadAssetIndex()
    {
        if (!await IndexDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(IndexDownloader)])
            );
            return false;
        }

        return true;
    }

    public static bool LoadAssetIndex()
    {
        assets = Json.Load<MCAssetsData>(MinecraftPathResolver.ClientIndexPath(launcherPath, versionDetails));
        if (assets == null)
        {
            NotificationService.Add(new Notification(NativeLogLevel.Error, "error.readfile", [nameof(MCAssetsData)]));
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadResources()
    {
        if (!await ResourceDownloader.Download(launcherPath, configUrls, assets))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(ResourceDownloader)])
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLogging()
    {
        if (!await LoggingDownloader.Download(launcherPath, versionDetails))
        {
            NotificationService.Add(
                new Notification(NativeLogLevel.Error, "error.download", [nameof(LoggingDownloader)])
            );
            return false;
        }

        return true;
    }
}