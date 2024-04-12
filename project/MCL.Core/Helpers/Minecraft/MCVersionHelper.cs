using MCL.Core.Handlers.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class MCVersionHelper
{
    public static MCVersion GetVersion(MCLauncherVersion launcherVersion, MCVersionManifest versionManifest)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return null;

        if (!MCVersionHelperErr.Exists(versionManifest))
            return null;

        foreach (MCVersion item in versionManifest.Versions)
        {
            if (item.ID == launcherVersion.Version)
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
