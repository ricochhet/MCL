using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.Minecraft.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class VersionHelper
{
    public static async Task<bool> SetVersion(
        Settings settings,
        LauncherVersion launcherVersion,
        bool updateVersionManifest = false
    )
    {
        MDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherSettings,
            settings.LauncherInstance,
            settings.MUrls
        );
        if (!MDownloadService.LoadVersionManifestWithoutLogging() || updateVersionManifest)
        {
            await MDownloadService.DownloadVersionManifest();
            MDownloadService.LoadVersionManifest();
        }

        if (MDownloadService.VersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(MDownloadService.VersionManifest);
        string version = launcherVersion.Version;

        if (version == "latest" || string.IsNullOrWhiteSpace(version))
            version = versions[0];

        if (!versions.Contains(version))
            return false;

        settings.LauncherVersion.Version = version;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetVersionIds(MVersionManifest versionManifest)
    {
        if (ObjectValidator<List<MVersion>>.IsNullOrEmpty(versionManifest?.Versions))
            return [];

        List<string> versions = [];
        foreach (MVersion item in versionManifest.Versions)
            versions.Add(item.ID);

        return versions;
    }

    public static MVersion GetVersion(LauncherVersion launcherVersion, MVersionManifest versionManifest)
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(launcherVersion?.Version)
            || ObjectValidator<List<MVersion>>.IsNullOrEmpty(versionManifest?.Versions)
        )
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
        if (ObjectValidator<string>.IsNullOrWhitespace(launcherVersion.Version))
            return null;

        MVersionManifest versionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(launcherPath));

        if (versionManifest?.Versions == null)
            return null;

        MVersion version = GetVersion(launcherVersion, versionManifest);
        if (ObjectValidator<MVersion>.IsNull(version))
            return null;

        MVersionDetails versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(launcherPath, version)
        );
        if (ObjectValidator<MVersionDetails>.IsNull(versionDetails))
            return null;
        return versionDetails;
    }
}
