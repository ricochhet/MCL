using MCL.Core.Extensions.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class AssetHelper
{
    public static string GetAssetId(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (!launcherVersion.VersionsExists())
            return string.Empty;

        MinecraftVersionManifest versionManifest = Json.Load<MinecraftVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest == null)
            return string.Empty;

        MinecraftVersion version = VersionHelper.GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return string.Empty;

        MinecraftVersionDetails versionDetails = Json.Load<MinecraftVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );

        if (versionDetails == null)
            return string.Empty;

        if (string.IsNullOrWhiteSpace(versionDetails.Assets))
            return string.Empty;
        return versionDetails.Assets;
    }
}
