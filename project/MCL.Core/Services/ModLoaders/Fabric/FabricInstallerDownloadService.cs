using System.Threading.Tasks;
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

public class FabricInstallerDownloadService : IJarDownloadService<FabricUrls>, IDownloadService
{
    public static FabricIndex FabricIndex { get; private set; }
    public static FabricInstaller FabricInstaller { get; private set; }
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

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
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

    public static bool LoadVersion()
    {
        if (!Loaded)
            return false;

        FabricInstaller = FabricVersionHelper.GetInstallerVersion(LauncherVersion, FabricIndex);
        if (FabricInstaller == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.FabricInstallerVersion, nameof(Models.ModLoaders.Fabric.FabricInstaller)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!Loaded)
            return false;

        if (!await FabricInstallerDownloader.Download(LauncherPath, LauncherVersion, FabricInstaller))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricInstallerDownloader)]);
            return false;
        }

        return true;
    }
}
