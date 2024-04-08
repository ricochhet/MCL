namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion
{
    public string MCVersion { get; set; }
    public string FabricInstallerVersion { get; set; }
    public string FabricLoaderVersion { get; set; }

    public static bool Exists(MCLauncherVersion minecraftVersion)
    {
        if (minecraftVersion == null)
            return false;

        if (string.IsNullOrEmpty(minecraftVersion.MCVersion))
            return false;

        if (string.IsNullOrEmpty(minecraftVersion.FabricInstallerVersion))
            return false;

        if (string.IsNullOrEmpty(minecraftVersion.FabricLoaderVersion))
            return false;

        return true;
    }
}
