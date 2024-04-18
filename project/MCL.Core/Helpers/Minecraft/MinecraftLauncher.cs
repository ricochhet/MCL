using MCL.Core.Extensions.Launcher;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Modding;

namespace MCL.Core.Helpers.Minecraft;

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
        if (!launcherVersion.VersionsExists())
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
