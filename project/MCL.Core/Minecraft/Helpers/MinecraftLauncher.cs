using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class MinecraftLauncher
{
    public static void Launch(Settings settings)
    {
        if (ObjectValidator<string>.IsNullOrWhitespace(settings?.LauncherVersion?.Version))
            return;

        settings.Save(settings.LauncherSettings.ClientType, LaunchArgs.DefaultJvmArguments(settings));
        if (VersionHelper.GetVersionDetails(settings.LauncherPath, settings.LauncherVersion) == null)
            return;
        JavaLauncher.Launch(
            settings,
            settings.LauncherPath.Path,
            settings.LauncherSettings.ClientType,
            JavaVersionHelper.GetDownloadedMCVersionJava(
                settings.LauncherPath,
                settings.LauncherVersion,
                settings.LauncherSettings
            )
        );
    }
}
