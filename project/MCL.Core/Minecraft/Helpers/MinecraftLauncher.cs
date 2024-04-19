using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Modding.Services;

namespace MCL.Core.Minecraft.Helpers;

public static class MinecraftLauncher
{
    public static void Launch(Settings settings)
    {
        if (!settings.LauncherVersion.VersionExists())
            return;

        settings.Save(settings.LauncherSettings.ClientType, LaunchArgs.DefaultJvmArguments(settings));
        SettingsService.Save(settings);
        settings.Save(ModdingService.ModSettings);
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
