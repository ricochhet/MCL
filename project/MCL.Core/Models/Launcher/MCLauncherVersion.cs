namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion(
    string version,
    string versionType,
    string fabricInstallerVersion,
    string fabricLoaderVersion,
    string quiltInstallerVersion,
    string quiltLoaderVersion
)
{
    public string Version { get; set; } = version;
    public string VersionType { get; set; } = versionType;
    public string FabricInstallerVersion { get; set; } = fabricInstallerVersion;
    public string FabricLoaderVersion { get; set; } = fabricLoaderVersion;
    public string QuiltInstallerVersion { get; set; } = quiltInstallerVersion;
    public string QuiltLoaderVersion { get; set; } = quiltLoaderVersion;

    public static bool Exists(MCLauncherVersion launcherVersion)
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

        return true;
    }
}
