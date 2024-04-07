namespace MCL.Core.Models.Launcher;

public class MCLauncherVersion
{
    public string MCVersion { get; set; }
    public string FabricVersion { get; set; }

    public static bool Exists(MCLauncherVersion minecraftVersion)
    {
        if (minecraftVersion == null)
            return false;

        if (string.IsNullOrEmpty(minecraftVersion.MCVersion))
            return false;

        return true;
    }
}
