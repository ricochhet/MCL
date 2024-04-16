using System.Collections.Generic;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
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
        if (!MCLauncherPath.Exists(launcherPath))
            return null;

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
