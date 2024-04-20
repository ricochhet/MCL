using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricInstallerArgs
{
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricInstallerType installerType
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                launcherVersion?.Version,
                launcherVersion?.FabricInstallerVersion,
                launcherVersion?.FabricLoaderVersion
            )
        )
            return null;

        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            "-jar \"{0}\" {1}",
            [FabricPathResolver.InstallerPath(launcherPath, launcherVersion), "client"]
        );
        jvmArguments.Add("-dir \"{0}\" {1}", [launcherPath.Path, FabricInstallerTypeResolver.ToString(installerType)]);
        jvmArguments.Add("-mcversion {0}", [launcherVersion.Version]);
        jvmArguments.Add("-loader {0}", [launcherVersion.FabricLoaderVersion]);
        jvmArguments.Add("-noprofile");

        return jvmArguments;
    }
}
