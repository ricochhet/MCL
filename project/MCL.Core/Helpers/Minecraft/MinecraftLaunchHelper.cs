using MCL.Core.Extensions;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Models.Launcher;
using MCL.Core.Services.Modding;

namespace MCL.Core.Helpers.Minecraft;

public static class MinecraftLaunchHelper
{
    public static void Launch(MCLauncher launcher, Config config)
    {
        config.Save(launcher.ClientType, LaunchArgsHelper.Default(launcher));
        config.Save(ModdingService.ModConfig);
        if (MCVersionHelper.GetVersionDetails(launcher.MCLauncherPath, launcher.MCLauncherVersion) == null)
            return;
        JavaLaunchHelper.Launch(
            config,
            launcher.MCLauncherPath.Path,
            launcher.ClientType,
            JavaVersionHelper.GetDownloadedMCVersionJava(
                launcher.MCLauncherPath,
                launcher.MCLauncherVersion,
                launcher.JavaRuntimeType
            )
        );
    }
}
