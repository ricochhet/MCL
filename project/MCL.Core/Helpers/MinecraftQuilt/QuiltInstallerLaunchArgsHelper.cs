using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public static class QuiltInstallerLaunchArgsHelper
{
    public static JvmArguments Default(MCLauncher launcher)
    {
        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            new LaunchArg(
                "-jar \"{0}\"",
                [
                    MinecraftQuiltPathResolver.DownloadedQuiltInstallerPath(
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion
                    ),
                ]
            )
        );
        jvmArguments.Add(
            new LaunchArg(
                "install client {0} {1}",
                [launcher.MCLauncherVersion.Version, launcher.MCLauncherVersion.QuiltLoaderVersion]
            )
        );
        jvmArguments.Add(new LaunchArg("--install-dir=\"{0}\"", [launcher.MCLauncherPath.Path]));
        jvmArguments.Add(new LaunchArg("--no-profile"));

        return jvmArguments;
    }
}
