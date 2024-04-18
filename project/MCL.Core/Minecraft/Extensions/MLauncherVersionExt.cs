using MCL.Core.Launcher.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MLauncherVersionExt
{
    public static bool VersionsExists(this LauncherVersion launcherVersion)
    {
        if (
            string.IsNullOrWhiteSpace(launcherVersion?.Version)
            || string.IsNullOrWhiteSpace(launcherVersion.VersionType)
        )
            return false;

        if (
            string.IsNullOrWhiteSpace(launcherVersion.FabricInstallerVersion)
            || string.IsNullOrWhiteSpace(launcherVersion.FabricLoaderVersion)
        )
            return false;

        if (
            string.IsNullOrWhiteSpace(launcherVersion.QuiltInstallerVersion)
            || string.IsNullOrWhiteSpace(launcherVersion.QuiltLoaderVersion)
        )
            return false;

        return !string.IsNullOrWhiteSpace(launcherVersion.PaperServerVersion);
    }
}
