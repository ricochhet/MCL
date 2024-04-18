using MCL.Core.Enums.MinecraftQuilt;
using MCL.Core.Extensions.Java;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.ModLoaders.Quilt;

namespace MCL.Core.Helpers.ModLoaders.Quilt;

public static class QuiltInstallerArgs
{
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltInstallerType installerType
    )
    {
        if (!launcherVersion.VersionsExists())
            return default;

        JvmArguments jvmArguments = new();
        jvmArguments.Add("-jar \"{0}\"", [QuiltPathResolver.DownloadedInstallerPath(launcherPath, launcherVersion)]);
        jvmArguments.Add(
            $"install {QuiltInstallerTypeResolver.ToString(installerType)} {0} {1}",
            [launcherVersion.Version, launcherVersion.QuiltLoaderVersion]
        );
        jvmArguments.Add(installerType, QuiltInstallerType.SERVER, "--download-server");
        jvmArguments.Add("--install-dir=\"{0}\"", [launcherPath.Path]);
        jvmArguments.Add("--no-profile");

        return jvmArguments;
    }
}
