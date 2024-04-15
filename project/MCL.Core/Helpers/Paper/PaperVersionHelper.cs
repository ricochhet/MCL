using System.Collections.Generic;
using MCL.Core.Handlers.Paper;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Paper;

namespace MCL.Core.Helpers.Paper;

public static class PaperVersionHelper
{
    public static List<string> GetVersionIds(PaperVersionManifest paperVersionManifest)
    {
        if (!PaperVersionHelperErr.Exists(paperVersionManifest))
            return [];

        List<string> versions = [];
        foreach (PaperBuild item in paperVersionManifest.Builds)
        {
            versions.Add(item.Build.ToString());
        }

        return versions;
    }

    public static PaperBuild GetVersion(MCLauncherVersion paperServerVersion, PaperVersionManifest paperVersionManifest)
    {
        if (!MCLauncherVersion.Exists(paperServerVersion))
            return null;

        if (!PaperVersionHelperErr.Exists(paperVersionManifest))
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
