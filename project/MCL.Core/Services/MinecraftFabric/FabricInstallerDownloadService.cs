using System.Threading.Tasks;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Interfaces.Services.MinecraftFabric;
using MCL.Core.Interfaces.Web;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Services.Launcher;
using MCL.Core.Web.MinecraftFabric;

namespace MCL.Core.Services.MinecraftFabric;

public class FabricInstallerDownloadService : IFabricInstallerDownloadService<MCFabricConfigUrls>, IDownloadService
{
    public static MCFabricIndex FabricIndex { get; private set; }
    public static MCFabricInstaller FabricInstaller { get; private set; }
    private static MCLauncherPath LauncherPath;
    private static MCLauncherVersion LauncherVersion;
    private static MCFabricConfigUrls FabricConfigUrls;
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

    public static async Task<bool> Download(bool useLocalVersionManifest = false)
    {
        if (!Loaded)
            return false;

        if (!useLocalVersionManifest && !await DownloadIndex())
            return false;

        if (!LoadIndex())
            return false;

        if (!LoadInstallerVersion())
            return false;

        if (!await DownloadInstaller())
            return false;

        return true;
    }

    public static async Task<bool> DownloadIndex()
    {
        if (!Loaded)
            return false;

        if (!await FabricIndexDownloader.Download(LauncherPath, FabricConfigUrls))
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

        FabricIndex = Json.Load<MCFabricIndex>(FabricPathResolver.DownloadedIndexPath(LauncherPath));
        if (FabricIndex == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(MCFabricIndex)]);
            return false;
        }

        return true;
    }

    public static bool LoadInstallerVersion()
    {
        if (!Loaded)
            return false;

        FabricInstaller = FabricVersionHelper.GetInstallerVersion(LauncherVersion, FabricIndex);
        if (FabricInstaller == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.FabricInstallerVersion, nameof(MCFabricInstaller)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadInstaller()
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
