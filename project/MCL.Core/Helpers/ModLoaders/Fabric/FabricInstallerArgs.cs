using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.ModLoaders.Fabric;

namespace MCL.Core.Helpers.ModLoaders.Fabric;

public static class FabricInstallerArgs
{
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        FabricInstallerType installerType
    )
    {
        if (!launcherVersion.VersionsExists())
            return default;

        JvmArguments jvmArguments = new();
        jvmArguments.Add(
            "-jar \"{0}\" {1}",
            [FabricPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion), "client"]
        );
        jvmArguments.Add("-dir \"{0}\" {1}", [launcherPath.Path, FabricInstallerTypeResolver.ToString(installerType)]);
        jvmArguments.Add("-mcversion {0}", [launcherVersion.Version]);
        jvmArguments.Add("-loader {0}", [launcherVersion.FabricLoaderVersion]);
        jvmArguments.Add("-noprofile");

        return jvmArguments;
    }
}
