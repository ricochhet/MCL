using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Web;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Services;

public class MDownloadService : IDownloadService
{
    public static MVersionManifest VersionManifest { get; private set; }
    public static MVersionDetails VersionDetails { get; private set; }
    public static MVersion Version { get; private set; }
    private static MAssetsData _assets;
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static LauncherSettings _launcherSettings;
    private static MUrls _mUrls;
    private static bool _loaded = false;

    public static void Init(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherSettings launcherSettings,
        MUrls mUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherSettings = launcherSettings;
        _mUrls = mUrls;
        _loaded = true;
    }

#pragma warning disable IDE0079
#pragma warning disable S3776
    public static async Task<bool> Download(bool useLocalVersionManifest = false)
#pragma warning restore
    {
        if (!_loaded)
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
        if (!_loaded)
            return false;

        if (!await VersionManifestDownloader.Download(_launcherPath, _mUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(MVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        VersionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(_launcherPath));
        if (VersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        VersionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(_launcherPath));
        if (VersionManifest == null)
            return false;

        return true;
    }

    public static bool LoadVersion()
    {
        if (!_loaded)
            return false;

        Version = VersionHelper.GetVersion(_launcherVersion, VersionManifest);
        if (Version == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.parse", [_launcherVersion?.Version, nameof(MVersion)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadVersionDetails()
    {
        if (!_loaded)
            return false;

        if (!await VersionDetailsDownloader.Download(_launcherPath, Version))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(VersionDetailsDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionDetails()
    {
        if (!_loaded)
            return false;

        VersionDetails = Json.Load<MVersionDetails>(MPathResolver.VersionDetailsPath(_launcherPath, Version));
        if (VersionDetails == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MVersionDetails)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLibraries()
    {
        if (!_loaded)
            return false;

        if (!await LibraryDownloader.Download(_launcherPath, _launcherSettings, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(LibraryDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClient()
    {
        if (!_loaded)
            return false;

        if (!await ClientDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ClientDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadClientMappings()
    {
        if (!_loaded)
            return false;

        if (!await ClientMappingsDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ClientMappingsDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServer()
    {
        if (!_loaded)
            return false;

        if (!await ServerDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ServerDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadServerMappings()
    {
        if (!_loaded)
            return false;

        if (!await ServerMappingsDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ServerMappingsDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadAssetIndex()
    {
        if (!_loaded)
            return false;

        if (!await AssetIndexDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(AssetIndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadAssetIndex()
    {
        if (!_loaded)
            return false;

        _assets = Json.Load<MAssetsData>(MPathResolver.ClientIndexPath(_launcherPath, VersionDetails));
        if (_assets == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MAssetsData)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadResources()
    {
        if (!_loaded)
            return false;

        if (!await ResourceDownloader.Download(_launcherPath, _mUrls, _assets))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(ResourceDownloader)]);
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLogging()
    {
        if (!_loaded)
            return false;

        if (!await LoggingDownloader.Download(_launcherPath, VersionDetails))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(LoggingDownloader)]);
            return false;
        }

        return true;
    }
}
