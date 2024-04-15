using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public class FabricInstallerLaunchArgsHelper : IFabricLaunchArgsHelper
{
    public static JvmArguments Default(MCLauncher launcher, FabricInstallerType installerType)
    {
        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            new LaunchArg(
                "-jar \"{0}\" {1}",
                [
                    MinecraftFabricPathResolver.DownloadedInstallerPath(
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion
                    ),
                    "client"
                ]
            )
        );
        jvmArguments.Add(
            new LaunchArg(
                "-dir \"{0}\" {1}",
                [launcher.MCLauncherPath.Path, FabricInstallerTypeResolver.ToString(installerType)]
            )
        );
        jvmArguments.Add(new LaunchArg("-mcversion {0}", [launcher.MCLauncherVersion.Version]));
        jvmArguments.Add(new LaunchArg("-loader {0}", [launcher.MCLauncherVersion.FabricLoaderVersion]));
        jvmArguments.Add(new LaunchArg("-noprofile"));

        return jvmArguments;
    }
}
