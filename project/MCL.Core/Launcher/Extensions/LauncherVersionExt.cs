using MCL.Core.Launcher.Models;

namespace MCL.Core.Launcher.Extensions;

public static class LauncherVersionExt
{
    public static bool VersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.Version);
    }

    public static bool VersionTypeExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.VersionType);
    }

    public static bool FabricInstallerVersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.FabricInstallerVersion);
    }

    public static bool FabricLoaderVersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.FabricLoaderVersion);
    }

    public static bool QuiltInstallerVersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.QuiltInstallerVersion);
    }

    public static bool QuiltLoaderVersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.QuiltLoaderVersion);
    }

    public static bool PaperServerVersionExists(this LauncherVersion launcherVersion)
    {
        return string.IsNullOrWhiteSpace(launcherVersion?.PaperServerVersion);
    }
}
