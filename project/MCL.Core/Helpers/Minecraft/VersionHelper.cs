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
    public static async Task<bool> SetVersions(Config config, string[] args)
    {
        MinecraftDownloadService.Init(
            config.LauncherPath,
            config.LauncherVersion,
            config.LauncherSettings,
            config.MinecraftUrls
        );
        if (!MinecraftDownloadService.LoadVersionManifest(true))
        {
            await MinecraftDownloadService.DownloadVersionManifest();
            MinecraftDownloadService.LoadVersionManifest(false);
        }

        if (MinecraftDownloadService.VersionManifest == null)
            return false;

        List<string> versions = GetVersionIds(MinecraftDownloadService.VersionManifest);
        string version = args[(int)VersionArgs.MINECRAFT];

        if (version == "latest")
            version = versions[0];

        if (!versions.Contains(version))
            return false;

        config.LauncherVersion.Version = version;
        ConfigService.Save(config);
        return true;
    }

    public static List<string> GetVersionIds(MCVersionManifest versionManifest)
    {
        if (!versionManifest.VersionsExists())
            return [];

        List<string> versions = [];
        foreach (MCVersion item in versionManifest.Versions)
        {
            versions.Add(item.ID);
        }

        return versions;
    }

    public static MCVersion GetVersion(MCLauncherVersion launcherVersion, MCVersionManifest versionManifest)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return null;

        if (!versionManifest.VersionsExists())
            return null;

        foreach (MCVersion item in versionManifest.Versions)
        {
            if (string.IsNullOrWhiteSpace(launcherVersion.Version) && item.ID == versionManifest.Latest.Release)
                return item;

            if ((!string.IsNullOrWhiteSpace(launcherVersion.Version)) && item.ID == launcherVersion.Version)
                return item;
        }
        return null;
    }

    public static MCVersionDetails GetVersionDetails(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return null;

        MCVersionManifest versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest?.Versions == null)
            return null;

        MCVersion version = GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return null;

        MCVersionDetails versionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );
        if (versionDetails == null)
            return null;
        return versionDetails;
    }
}
