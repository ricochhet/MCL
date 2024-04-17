namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion
{
    public string Version { get; set; } = string.Empty;
    public string VersionType { get; set; } = "release";
    public string FabricInstallerVersion { get; set; } = string.Empty;
    public string FabricLoaderVersion { get; set; } = string.Empty;
    public string QuiltInstallerVersion { get; set; } = string.Empty;
    public string QuiltLoaderVersion { get; set; } = string.Empty;
    public string PaperServerVersion { get; set; } = string.Empty;

    public MCLauncherVersion() { }

    public MCLauncherVersion(
        string version,
        string versionType,
        string fabricInstallerVersion,
        string fabricLoaderVersion,
        string quiltInstallerVersion,
        string quiltLoaderVersion,
        string paperServerVersion
    )
    {
        Version = version;
        VersionType = versionType;
        FabricInstallerVersion = fabricInstallerVersion;
        FabricLoaderVersion = fabricLoaderVersion;
        QuiltInstallerVersion = quiltInstallerVersion;
        QuiltLoaderVersion = quiltLoaderVersion;
        PaperServerVersion = paperServerVersion;
    }

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

        if (string.IsNullOrWhiteSpace(launcherVersion.PaperServerVersion))
            return false;

        return true;
    }
}
