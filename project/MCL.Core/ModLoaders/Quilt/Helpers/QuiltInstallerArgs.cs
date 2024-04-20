using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Enums;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public static class QuiltInstallerArgs
{
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltInstallerType installerType
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhitespace(
                launcherVersion?.Version,
                launcherVersion?.QuiltInstallerVersion,
                launcherVersion?.QuiltLoaderVersion
            )
        )
            return null;

        JvmArguments jvmArguments = new();
        jvmArguments.Add("-jar \"{0}\"", [QuiltPathResolver.InstallerPath(launcherPath, launcherVersion)]);
        jvmArguments.Add(
            $"install {QuiltInstallerTypeResolver.ToString(installerType)} {0} {1}",
            [launcherVersion.Version, launcherVersion.QuiltLoaderVersion]
        );
        jvmArguments.Add(installerType, QuiltInstallerType.INSTALL_SERVER, "--download-server");
        jvmArguments.Add("--install-dir=\"{0}\"", [launcherPath.Path]);
        jvmArguments.Add("--no-profile");

        return jvmArguments;
    }
}
