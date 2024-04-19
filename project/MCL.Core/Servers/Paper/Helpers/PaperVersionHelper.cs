using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Servers.Paper.Extensions;
using MCL.Core.Servers.Paper.Models;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperVersionHelper
{
    public static async Task<bool> SetVersion(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        PaperServerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.PaperUrls);
        if (!PaperServerDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await PaperServerDownloadService.DownloadVersionManifest();
            PaperServerDownloadService.LoadVersionManifest();
        }

        if (PaperServerDownloadService.PaperVersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(PaperServerDownloadService.PaperVersionManifest);
        string version = launcherVersion.PaperServerVersion;

        if (version == "latest" || string.IsNullOrWhiteSpace(version))
            version = versions[^1]; // Latest is the last version of the array.

        if (!versions.Contains(version))
            return false;

        settings.LauncherVersion.PaperServerVersion = version;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetVersionIds(PaperVersionManifest paperVersionManifest)
    {
        if (!paperVersionManifest.BuildsExists())
            return [];

        List<string> versions = [];
        foreach (PaperBuild item in paperVersionManifest.Builds)
        {
            versions.Add(item.Build.ToString());
        }

        return versions;
    }

    public static PaperBuild GetVersion(LauncherVersion paperServerVersion, PaperVersionManifest paperVersionManifest)
    {
        if (!paperServerVersion.PaperServerVersionExists())
            return null;

        if (!paperVersionManifest.BuildsExists())
            return null;

        PaperBuild paperBuild = paperVersionManifest.Builds[^1];
        foreach (PaperBuild item in paperVersionManifest.Builds)
        {
            if (item.Build.ToString() == paperServerVersion.PaperServerVersion)
                return item;
        }
        return paperBuild;
    }
}
