using MCL.Core.Models.Launcher;

namespace MCL.Core.Extensions.Minecraft;

public static class MCLauncherVersionExt
{
    public static bool VersionsExists(this MCLauncherVersion launcherVersion)
    {
        if (launcherVersion == null)
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.Version))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.VersionType))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.FabricInstallerVersion))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.FabricLoaderVersion))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.QuiltInstallerVersion))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.QuiltLoaderVersion))
            return false;

        if (string.IsNullOrWhiteSpace(launcherVersion.PaperServerVersion))
            return false;

        return true;
    }
}
