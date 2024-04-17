using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class AssetHelper
{
    public static string GetAssetId(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return string.Empty;

        MCVersionManifest versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest == null)
            return string.Empty;

        MCVersion version = VersionHelper.GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return string.Empty;

        MCVersionDetails versionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );

        if (versionDetails == null)
            return string.Empty;

        if (string.IsNullOrWhiteSpace(versionDetails.Assets))
            return string.Empty;
        return versionDetails.Assets;
    }
}
