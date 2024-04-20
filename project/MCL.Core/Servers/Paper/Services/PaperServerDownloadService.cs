using System.Threading.Tasks;
using MCL.Core.Interfaces.Web;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Resolvers;
using MCL.Core.Servers.Paper.Web;

namespace MCL.Core.Servers.Paper.Services;

public class PaperServerDownloadService : IDownloadService
{
    public static PaperVersionManifest PaperVersionManifest { get; private set; }
    public static PaperBuild PaperBuild { get; private set; }
    private static LauncherPath _launcherPath;
    private static LauncherVersion _launcherVersion;
    private static LauncherInstance _launcherInstance;
    private static PaperUrls _paperUrls;
    private static bool _loaded = false;

    public static void Init(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        PaperUrls paperUrls
    )
    {
        _launcherPath = launcherPath;
        _launcherVersion = launcherVersion;
        _launcherInstance = launcherInstance;
        _paperUrls = paperUrls;
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

        foreach (string version in _launcherInstance.PaperServerVersions)
        {
            if (version == _launcherVersion.PaperServerVersion)
                _launcherInstance.PaperServerVersions.Remove(version);
        }

        _launcherInstance.PaperServerVersions.Add(_launcherVersion.PaperServerVersion);
        SettingsService.Load().Save(_launcherInstance);

        return true;
    }

    public static async Task<bool> DownloadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (!await PaperVersionManifestDownloader.Download(_launcherPath, _launcherVersion, _paperUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", nameof(PaperVersionManifestDownloader));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifest()
    {
        if (!_loaded)
            return false;

        if (ObjectValidator<string>.IsNullOrWhitespace(_launcherVersion?.Version))
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.VersionManifestPath(_launcherPath, _launcherVersion)
        );
        if (PaperVersionManifest == null)
        {
            NotificationService.Log(NativeLogLevel.Error, "error.readfile", nameof(PaperVersionManifest));
            return false;
        }

        return true;
    }

    public static bool LoadVersionManifestWithoutLogging()
    {
        if (!_loaded)
            return false;

        if (ObjectValidator<string>.IsNullOrWhitespace(_launcherVersion?.Version))
            return false;

        PaperVersionManifest = Json.Load<PaperVersionManifest>(
            PaperPathResolver.VersionManifestPath(_launcherPath, _launcherVersion)
        );
        if (PaperVersionManifest == null)
            return false;

        return true;
    }

    public static bool LoadVersion()
    {
        if (!_loaded)
            return false;

        PaperBuild = PaperVersionHelper.GetVersion(_launcherVersion, PaperVersionManifest);
        if (PaperBuild == null)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.parse",
                _launcherVersion?.PaperServerVersion,
                nameof(PaperBuild)
            );
            return false;
        }

        return true;
    }

    public static async Task<bool> DownloadJar()
    {
        if (!_loaded)
            return false;

        if (!await PaperServerDownloader.Download(_launcherPath, _launcherVersion, PaperBuild, _paperUrls))
        {
            NotificationService.Log(NativeLogLevel.Error, "error.download", nameof(PaperServerDownloader));
            return false;
        }

        return true;
    }
}
