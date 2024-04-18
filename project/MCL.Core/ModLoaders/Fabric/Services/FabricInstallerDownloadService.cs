using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Fabric.Web;

namespace MCL.Core.ModLoaders.Fabric.Services;

public class FabricInstallerDownloadService : IJarDownloadService<FabricUrls>, IDownloadService
{
    public static FabricVersionManifest FabricVersionManifest { get; private set; }
    public static FabricInstaller FabricInstaller { get; private set; }
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

        if (!LoadVersion())
            return false;

        if (!await DownloadJar())
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

    public static bool LoadVersion()
    {
        if (!_loaded)
            return false;

        FabricInstaller = FabricVersionHelper.GetInstallerVersion(_launcherVersion, FabricVersionManifest);
        if (FabricInstaller == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [_launcherVersion?.FabricInstallerVersion, nameof(FabricInstaller)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!_loaded)
            return false;

        if (!await FabricInstallerDownloader.Download(_launcherPath, _launcherVersion, FabricInstaller))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(FabricInstallerDownloader)]);
            return false;
        }

        return true;
    }
}
