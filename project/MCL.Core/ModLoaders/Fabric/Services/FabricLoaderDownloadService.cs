using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricLoaderDownloadService : ILoaderDownloadService<FabricUrls>, IDownloadService
{
    public static FabricVersionManifest FabricVersionManifest { get; private set; }
    public static FabricProfile FabricProfile { get; private set; }
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static FabricUrls _fabricUrls;
    private static bool _loaded = false;

    public static void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, FabricUrls fabricUrls)
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _fabricUrls = fabricUrls;
        _loaded = true;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!_loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadVersionManifest())
            return false;

        if (!LoadVersionManifest())
            return false;

        if (!await DownloadProfile())
            return false;

        if (!LoadProfile())
            return false;

        if (!LoadLoaderVersion())
            return false;

        if (!await DownloadLoader())
            return false;

        return true;
    }

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await FabricVersionManifestDownloader.Download(_launcherPath, _fabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricVersionManifestDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (FabricVersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(FabricVersionManifest)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        FabricVersionManifest = Json.Load<FabricVersionManifest>(FabricPathResolver.VersionManifestPath(_launcherPath));
        if (FabricVersionManifest == null)
            return false;

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!_loaded)
            return false;

        if (!await FabricProfileDownloader.Download(_launcherPath, _launcherVersion, _fabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricProfileDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!_loaded)
            return false;

        if (!_launcherVersion.VersionsExists())
            return false;

        FabricProfile = Json.Load<FabricProfile>(FabricPathResolver.ProfilePath(_launcherPath, _launcherVersion));
        if (FabricProfile == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricProfile)]);
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!_loaded)
            return false;

        FabricLoader fabricLoader = FabricVersionHelper.GetLoaderVersion(_launcherVersion, FabricVersionManifest);
        if (fabricLoader == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [_launcherVersion?.FabricLoaderVersion, nameof(FabricLoader)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!_loaded)
            return false;

        if (!await FabricLoaderDownloader.Download(_launcherPath, _launcherVersion, FabricProfile, _fabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricLoaderDownloader)]);
            return false;
        }

        return true;
    }
}
