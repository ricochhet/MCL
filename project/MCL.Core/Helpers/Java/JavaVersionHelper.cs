using MCL.Core.Enums.Java;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Java;

public static class JavaVersionHelper
{
    public static JavaRuntimeType GetDownloadedMCVersionJava(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        JavaRuntimeType fallback
    )
    {
        if (!MCLauncherPath.Exists(launcherPath))
            return fallback;

        if (!MCLauncherVersion.Exists(launcherVersion))
            return fallback;

        MCVersionManifest versionManifest = Json.Load<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(launcherPath)
        );

        if (versionManifest?.Versions == null)
            return fallback;

        MCVersion version = MCVersionHelper.GetVersion(launcherVersion, versionManifest);
        if (version == null)
            return fallback;

        MCVersionDetails versionDetails = Json.Load<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(launcherPath, version)
        );

        if (string.IsNullOrWhiteSpace(versionDetails?.JavaVersion?.Component))
            return fallback;
        return GenericEnumParser.Parse(versionDetails.JavaVersion.Component, JavaRuntimeType.JAVA_RUNTIME_GAMMA);
    }
}
