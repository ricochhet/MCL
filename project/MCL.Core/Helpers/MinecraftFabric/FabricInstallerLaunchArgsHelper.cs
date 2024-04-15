using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftFabric;

namespace MCL.Core.Helpers.MinecraftFabric;

public class FabricInstallerLaunchArgsHelper : IFabricLaunchArgsHelper
{
    public static JvmArguments Default(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        FabricInstallerType installerType
    )
    {
        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            new LaunchArg(
                "-jar \"{0}\" {1}",
                [FabricPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion), "client"]
            )
        );
        jvmArguments.Add(
            new LaunchArg("-dir \"{0}\" {1}", [launcherPath.Path, FabricInstallerTypeResolver.ToString(installerType)])
        );
        jvmArguments.Add(new LaunchArg("-mcversion {0}", [launcherVersion.Version]));
        jvmArguments.Add(new LaunchArg("-loader {0}", [launcherVersion.FabricLoaderVersion]));
        jvmArguments.Add(new LaunchArg("-noprofile"));

        return jvmArguments;
    }
}
