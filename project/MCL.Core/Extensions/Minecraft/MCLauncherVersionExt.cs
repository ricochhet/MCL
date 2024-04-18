using MCL.Core.Models.Launcher;

namespace MCL.Core.Extensions.Minecraft;

public static class MCLauncherVersionExt
{
    public static bool VersionsExists(this MCLauncherVersion launcherVersion)
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
