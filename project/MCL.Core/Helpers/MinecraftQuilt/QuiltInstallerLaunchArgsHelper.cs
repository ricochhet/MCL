using MCL.Core.Enums.MinecraftQuilt;
using MCL.Core.Extensions.Java;
using MCL.Core.Interfaces.Helpers.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.MinecraftFabric;
using MCL.Core.Resolvers.MinecraftQuilt;

namespace MCL.Core.Helpers.MinecraftQuilt;

public class QuiltInstallerLaunchArgsHelper : IFabricLaunchArgsHelper<QuiltInstallerType>
{
    public static JvmArguments Default(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        QuiltInstallerType installerType
    )
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return default;

        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            new LaunchArg("-jar \"{0}\"", [QuiltPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion),])
        );
        jvmArguments.Add(
            new LaunchArg(
                $"install {QuiltInstallerTypeResolver.ToString(installerType)} {0} {1}",
                [launcherVersion.Version, launcherVersion.QuiltLoaderVersion]
            )
        );
        jvmArguments.Add(installerType, QuiltInstallerType.SERVER, new LaunchArg("--download-server"));
        jvmArguments.Add(new LaunchArg("--install-dir=\"{0}\"", [launcherPath.Path]));
        jvmArguments.Add(new LaunchArg("--no-profile"));

        return jvmArguments;
    }
}
