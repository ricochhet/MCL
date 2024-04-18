using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class AssetHelper
{
    public static string GetAssetId(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (!launcherVersion.VersionsExists())
            return string.Empty;

        MVersionManifest versionManifest = Json.Load<MVersionManifest>(
            MPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest == null)
            return string.Empty;

        MVersion version = VersionHelper.GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return string.Empty;

        MVersionDetails versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );

        if (versionDetails == null)
            return string.Empty;

        if (string.IsNullOrWhiteSpace(versionDetails.Assets))
            return string.Empty;
        return versionDetails.Assets;
    }
}
