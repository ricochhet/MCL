namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion(
    string version,
    string versionType,
    string fabricInstallerVersion,
    string fabricLoaderVersion
)
{
    public string Version { get; set; } = version;
    public string VersionType { get; set; } = versionType;
    public string FabricInstallerVersion { get; set; } = fabricInstallerVersion;
    public string FabricLoaderVersion { get; set; } = fabricLoaderVersion;

    public static bool Exists(MCLauncherVersion launcherVersion)
    {
        if (launcherVersion == null)
            return false;

        if (string.IsNullOrEmpty(launcherVersion.Version))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.VersionType))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.FabricInstallerVersion))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.FabricLoaderVersion))
            return false;

        return true;
    }
}
