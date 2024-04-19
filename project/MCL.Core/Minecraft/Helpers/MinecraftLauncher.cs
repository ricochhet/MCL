using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Modding.Services;

namespace MCL.Core.Minecraft.Helpers;

public static class MinecraftLauncher
{
    public static void Launch(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherSettings launcherSettings,
        LauncherUsername launcherUsername,
        Settings settings
    )
    {
        if (!launcherVersion.VersionExists())
            return;

        settings.Save(
            launcherSettings.ClientType,
            LaunchArgs.DefaultJvmArguments(launcherPath, launcherVersion, launcherSettings, launcherUsername)
        );
        SettingsService.Save(settings);
        settings.Save(ModdingService.ModSettings);
        if (VersionHelper.GetVersionDetails(launcherPath, launcherVersion) == null)
            return;
        JavaLauncher.Launch(
            settings,
            launcherPath.Path,
            launcherSettings.ClientType,
            JavaVersionHelper.GetDownloadedMCVersionJava(launcherPath, launcherVersion, launcherSettings)
        );
    }
}
