using System.Threading.Tasks;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.ModLoaders.Fabric;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Resolvers.ModLoaders.Fabric;
using MCL.Core.Services.Interfaces;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.ModLoaders.Fabric;

namespace MCL.Core.Services.ModLoaders.Fabric;

public class FabricLoaderDownloadService : ILoaderDownloadService<FabricUrls>, IDownloadService
{
    public static FabricIndex FabricIndex { get; private set; }
    public static FabricProfile FabricProfile { get; private set; }
    private static LauncherPath LauncherPath;
    private static LauncherVersion LauncherVersion;
    private static FabricUrls FabricUrls;
    private static bool Loaded = false;

    public static void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, FabricUrls fabricUrls)
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        FabricUrls = fabricUrls;
        Loaded = true;
    }

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!Loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadIndex())
            return false;

        if (!LoadIndex())
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

    public static async Task<bool> DownloadIndex()
    {
        if (!Loaded)
            return false;

        if (!await FabricIndexDownloader.Download(LauncherPath, FabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricIndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        FabricIndex = Json.Load<FabricIndex>(FabricPathResolver.DownloadedIndexPath(LauncherPath));
        if (FabricIndex == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.readfile",
                [nameof(Models.ModLoaders.Fabric.FabricIndex)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadProfile()
    {
        if (!Loaded)
            return false;

        if (!await FabricProfileDownloader.Download(LauncherPath, LauncherVersion, FabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricProfileDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadProfile()
    {
        if (!Loaded)
            return false;

        if (!LauncherVersion.VersionsExists())
            return false;

        FabricProfile = Json.Load<FabricProfile>(
            FabricPathResolver.DownloadedProfilePath(LauncherPath, LauncherVersion)
        );
        if (FabricProfile == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.download",
                [nameof(Models.ModLoaders.Fabric.FabricProfile)]
            );
            return false;
        }

        return true;
    }

    public static bool LoadLoaderVersion()
    {
        if (!Loaded)
            return false;

        FabricLoader fabricLoader = FabricVersionHelper.GetLoaderVersion(LauncherVersion, FabricIndex);
        if (fabricLoader == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.FabricLoaderVersion, nameof(FabricLoader)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadLoader()
    {
        if (!Loaded)
            return false;

        if (!await FabricLoaderDownloader.Download(LauncherPath, LauncherVersion, FabricProfile, FabricUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricLoaderDownloader)]);
            return false;
        }

        return true;
    }
}
