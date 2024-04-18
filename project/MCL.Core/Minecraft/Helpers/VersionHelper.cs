using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class VersionHelper
{
    public static async Task<bool> SetVersions(Settings settings, string[] args)
    {
        MDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherSettings,
            settings.MUrls
        );
        if (!MDownloadService.LoadVersionManifest())
        {
            await MDownloadService.DownloadVersionManifest();
            MDownloadService.LoadVersionManifest();
        }

        if (MDownloadService.VersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(MDownloadService.VersionManifest);
        string version = args[(int)VersionArgs.MINECRAFT];

        if (version == "latest")
            version = versions[0];

        if (!versions.Contains(version))
            return false;

        settings.LauncherVersion.Version = version;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetVersionIds(MVersionManifest versionManifest)
    {
        if (!versionManifest.VersionsExists())
            return [];

        List<string> versions = [];
        foreach (MVersion item in versionManifest.Versions)
        {
            versions.Add(item.ID);
        }

        return versions;
    }

    public static MVersion GetVersion(LauncherVersion launcherVersion, MVersionManifest versionManifest)
    {
        if (!launcherVersion.VersionsExists())
            return null;

        if (!versionManifest.VersionsExists())
            return null;

        foreach (MVersion item in versionManifest.Versions)
        {
            if (string.IsNullOrWhiteSpace(launcherVersion.Version) && item.ID == versionManifest.Latest.Release)
                return item;

            if ((!string.IsNullOrWhiteSpace(launcherVersion.Version)) && item.ID == launcherVersion.Version)
                return item;
        }
        return null;
    }

    public static MVersionDetails GetVersionDetails(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (!launcherVersion.VersionsExists())
            return null;

        MVersionManifest versionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(launcherPath));

        if (versionManifest?.Versions == null)
            return null;

        MVersion version = GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return null;

        MVersionDetails versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(launcherPath, version)
        );
        if (versionDetails == null)
            return null;
        return versionDetails;
    }
}
