using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Extensions;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public class QuiltInstallerLaunchArgsHelper : IFabricLaunchArgsHelper
{
    public static JvmArguments Default(MCLauncher launcher, FabricInstallerType installerType)
    {
        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            new LaunchArg(
                "-jar \"{0}\"",
                [
                    MinecraftQuiltPathResolver.DownloadedInstallerPath(
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion
                    ),
                ]
            )
        );
        jvmArguments.Add(
            new LaunchArg(
                $"install {FabricInstallerTypeResolver.ToString(installerType)} {0} {1}",
                [launcher.MCLauncherVersion.Version, launcher.MCLauncherVersion.QuiltLoaderVersion]
            )
        );
        jvmArguments.Add(installerType, FabricInstallerType.SERVER, new LaunchArg("--download-server"));
        jvmArguments.Add(new LaunchArg("--install-dir=\"{0}\"", [launcher.MCLauncherPath.Path]));
        jvmArguments.Add(new LaunchArg("--no-profile"));

        return jvmArguments;
    }
}
