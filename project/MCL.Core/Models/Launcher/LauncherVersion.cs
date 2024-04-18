namespace MCL.Core.Models.Launcher;

public class LauncherVersion
{
    public string Version { get; set; } = string.Empty;
    public string VersionType { get; set; } = "release";
    public string FabricInstallerVersion { get; set; } = string.Empty;
    public string FabricLoaderVersion { get; set; } = string.Empty;
    public string QuiltInstallerVersion { get; set; } = string.Empty;
    public string QuiltLoaderVersion { get; set; } = string.Empty;
    public string PaperServerVersion { get; set; } = string.Empty;

    public LauncherVersion() { }

    public LauncherVersion(
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
}
