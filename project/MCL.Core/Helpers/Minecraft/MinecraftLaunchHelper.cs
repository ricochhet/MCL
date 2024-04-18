using MCL.Core.Extensions.Launcher;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Modding;

namespace MCL.Core.Helpers.Minecraft;

public static class MinecraftLaunchHelper
{
    public static void Launch(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCLauncherSettings launcherSettings,
        MCLauncherUsername launcherUsername,
        Config config
    )
    {
        if (!launcherVersion.VersionsExists())
            return;

        config.Save(
            launcherSettings.ClientType,
            LaunchArgsHelper.Default(launcherPath, launcherVersion, launcherSettings, launcherUsername)
        );
        ConfigService.Save(config);
        config.Save(ModdingService.ModConfig);
        if (VersionHelper.GetVersionDetails(launcherPath, launcherVersion) == null)
            return;
        JavaLaunchHelper.Launch(
            config,
            launcherPath.Path,
            launcherSettings.ClientType,
            JavaVersionHelper.GetDownloadedMCVersionJava(launcherPath, launcherVersion, launcherSettings)
        );
    }
}
