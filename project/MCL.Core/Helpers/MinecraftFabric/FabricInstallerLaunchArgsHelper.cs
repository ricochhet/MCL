using MCL.Core.Interfaces.Helpers.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public class FabricInstallerLaunchArgsHelper : ILaunchArgsHelper
{
    public static JvmArguments Default(MCLauncher launcher)
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
        jvmArguments.Add(new LaunchArg("-dir \"{0}\" {1}", [launcher.MCLauncherPath.Path, "client"]));
        jvmArguments.Add(new LaunchArg("-mcversion {0}", [launcher.MCLauncherVersion.Version]));
        jvmArguments.Add(new LaunchArg("-loader {0}", [launcher.MCLauncherVersion.FabricLoaderVersion]));
        jvmArguments.Add(new LaunchArg("-noprofile"));

        return jvmArguments;
    }
}
