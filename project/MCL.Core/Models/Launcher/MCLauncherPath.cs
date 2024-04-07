namespace MCL.Core.Models.Launcher;

public class MCLauncherPath
{
    public string MCPath { get; set; }
    public string FabricPath { get; set; }

    public static bool Exists(MCLauncherPath minecraftPath)
    {
        if (minecraftPath == null)
            return false;

        if (string.IsNullOrEmpty(minecraftPath.MCPath))
            return false;

        if (string.IsNullOrEmpty(minecraftPath.FabricPath))
            return false;

        return true;
    }
}
