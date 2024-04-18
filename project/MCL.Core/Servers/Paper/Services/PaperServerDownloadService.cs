using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MCL.Core.Servers.Paper.Web;

namespace MCL.Core.Servers.Paper.Services;

public class PaperServerDownloadService : IJarDownloadService<PaperUrls>, IDownloadService
{
    public static PaperVersionManifest PaperVersionManifest { get; private set; }
    public static PaperBuild PaperBuild { get; private set; }
    private static LauncherPath LauncherPath;
    private static LauncherVersion LauncherVersion;
    private static PaperUrls PaperUrls;
    private static bool Loaded = false;

    public static void Init(LauncherPath launcherPath, LauncherVersion launcherVersion, PaperUrls paperUrls)
    {
        LauncherPath = launcherPath;
        LauncherVersion = launcherVersion;
        PaperUrls = paperUrls;
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

        if (!await PaperIndexDownloader.Download(LauncherPath, LauncherVersion, PaperUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(PaperIndexDownloader)]);
            return false;
        }

        return true;
    }

    public static bool LoadIndex()
    {
        if (!Loaded)
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.DownloadedIndexPath(LauncherPath, LauncherVersion)
        );
        if (PaperVersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", [nameof(QuiltIndex)]);
            return false;
        }

        return true;
    }

    public static bool LoadVersion()
    {
        if (!Loaded)
            return false;

        PaperBuild = PaperVersionHelper.GetVersion(LauncherVersion, PaperVersionManifest);
        if (PaperBuild == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                [LauncherVersion?.FabricInstallerVersion, nameof(PaperBuild)]
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!Loaded)
            return false;

        if (!await PaperServerDownloader.Download(LauncherPath, LauncherVersion, PaperBuild, PaperUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", [nameof(PaperServerDownloader)]);
            return false;
        }

        return true;
    }
}
