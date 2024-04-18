using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Extensions.Paper;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Paper;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Paper;

namespace MCL.Core.Helpers.Paper;

public static class PaperVersionHelper
{
    public static async Task<bool> SetVersions(Settings settings, string[] args)
    {
        PaperServerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.PaperUrls);
        if (!PaperServerDownloadService.LoadIndex())
        {
            await PaperServerDownloadService.DownloadIndex();
            PaperServerDownloadService.LoadIndex();
        }

        if (PaperServerDownloadService.PaperVersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(PaperServerDownloadService.PaperVersionManifest);
        string version = args[(int)VersionArgs.PAPER_SERVER];

        if (version == "latest")
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
        if (!paperServerVersion.VersionsExists())
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
