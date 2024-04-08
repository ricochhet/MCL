namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion
{
    public string MCVersion { get; set; }
    public string MCVersionType { get; set; }
    public string FabricInstallerVersion { get; set; }
    public string FabricLoaderVersion { get; set; }

    public static bool Exists(MCLauncherVersion launcherVersion)
    {
        if (launcherVersion == null)
            return false;

        if (string.IsNullOrEmpty(launcherVersion.MCVersion))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.MCVersionType))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.FabricInstallerVersion))
            return false;

        if (string.IsNullOrEmpty(launcherVersion.FabricLoaderVersion))
            return false;

        return true;
    }
}
