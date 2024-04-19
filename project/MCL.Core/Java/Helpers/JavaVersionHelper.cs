using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Resolvers;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;

namespace MCL.Core.Java.Helpers;

public static class JavaVersionHelper
{
    public static JavaRuntimeType GetDownloadedMCVersionJava(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherSettings launcherSettings
    )
    {
        if (!launcherVersion.VersionExists())
            return launcherSettings.JavaRuntimeType;
        MVersionDetails versionDetails = VersionHelper.GetVersionDetails(launcherPath, launcherVersion);
        if (string.IsNullOrWhiteSpace(versionDetails?.JavaVersion?.Component))
            return launcherSettings.JavaRuntimeType;
        return GenericEnumParser.Parse(versionDetails.JavaVersion.Component, launcherSettings.JavaRuntimeType);
    }
}
