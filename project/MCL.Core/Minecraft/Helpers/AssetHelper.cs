using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class AssetHelper
{
    public static string GetAssetId(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([launcherVersion?.Version]))
            return string.Empty;

        MVersionManifest versionManifest = Json.Load<MVersionManifest>(MPathResolver.VersionManifestPath(launcherPath));

        if (ObjectValidator<MVersionManifest>.IsNull(versionManifest))
            return string.Empty;

        MVersion version = VersionHelper.GetVersion(launcherVersion, versionManifest);
        if (ObjectValidator<MVersion>.IsNull(version))
            return string.Empty;

        MVersionDetails versionDetails = Json.Load<MVersionDetails>(
            MPathResolver.VersionDetailsPath(launcherPath, version)
        );

        if (ObjectValidator<string>.IsNullOrWhiteSpace([versionDetails?.Assets]))
            return string.Empty;
        return versionDetails.Assets;
    }
}
