namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion
{
    public string Version { get; set; }
    public string VersionType { get; set; }
    public string FabricInstallerVersion { get; set; }
    public string FabricLoaderVersion { get; set; }

    public MCLauncherVersion(
        string version,
        string versionType,
        string fabricInstallerVersion,
        string fabricLoaderVersion
    )
    {
        Version = version;
        VersionType = versionType;
        FabricInstallerVersion = fabricInstallerVersion;
        FabricLoaderVersion = fabricLoaderVersion;
    }

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
