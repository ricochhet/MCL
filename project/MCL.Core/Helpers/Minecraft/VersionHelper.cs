using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static async Task<bool> SetVersions(Settings settings, string[] args)
    {
        MinecraftDownloadService.Init(
            settings.LauncherPath,
            settings.LauncherVersion,
            settings.LauncherSettings,
            settings.MinecraftUrls
        );
        if (!MinecraftDownloadService.LoadVersionManifest())
        {
            await MinecraftDownloadService.DownloadVersionManifest();
            MinecraftDownloadService.LoadVersionManifest();
        }

        if (MinecraftDownloadService.VersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(MinecraftDownloadService.VersionManifest);
        string version = args[(int)VersionArgs.MINECRAFT];

        if (version == "latest")
            version = versions[0];

        if (!versions.Contains(version))
            return false;

        settings.LauncherVersion.Version = version;
        SettingsService.Save(settings);
        return true;
    }

    public static List<string> GetVersionIds(MinecraftVersionManifest versionManifest)
    {
        if (!versionManifest.VersionsExists())
            return [];

        List<string> versions = [];
        foreach (MinecraftVersion item in versionManifest.Versions)
        {
            versions.Add(item.ID);
        }

        return versions;
    }

    public static MinecraftVersion GetVersion(LauncherVersion launcherVersion, MinecraftVersionManifest versionManifest)
    {
        if (!launcherVersion.VersionsExists())
            return null;

        if (!versionManifest.VersionsExists())
            return null;

        foreach (MinecraftVersion item in versionManifest.Versions)
        {
            if (string.IsNullOrWhiteSpace(launcherVersion.Version) && item.ID == versionManifest.Latest.Release)
                return item;

            if ((!string.IsNullOrWhiteSpace(launcherVersion.Version)) && item.ID == launcherVersion.Version)
                return item;
        }
        return null;
    }

    public static MinecraftVersionDetails GetVersionDetails(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (!launcherVersion.VersionsExists())
            return null;

        MinecraftVersionManifest versionManifest = Json.Load<MinecraftVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest?.Versions == null)
            return null;

        MinecraftVersion version = GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return null;

        MinecraftVersionDetails versionDetails = Json.Load<MinecraftVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );
        if (versionDetails == null)
            return null;
        return versionDetails;
    }
}
