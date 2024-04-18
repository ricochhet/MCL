using MCL.Core.Enums.Java;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Resolvers;

namespace MCL.Core.Helpers.Java;

public static class JavaVersionHelper
{
    public static JavaRuntimeType GetDownloadedMCVersionJava(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCLauncherSettings launcherSettings
    )
    {
        if (!launcherVersion.VersionsExists())
            return launcherSettings.JavaRuntimeType;
        MCVersionDetails versionDetails = VersionHelper.GetVersionDetails(launcherPath, launcherVersion);
        if (string.IsNullOrWhiteSpace(versionDetails?.JavaVersion?.Component))
            return launcherSettings.JavaRuntimeType;
        return GenericEnumParser.Parse(versionDetails.JavaVersion.Component, launcherSettings.JavaRuntimeType);
    }
}
