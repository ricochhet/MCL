namespace MCL.Core.Models.Launcher;

public class MCLauncherPath
{
    public string MCPath { get; set; }
    public string FabricInstallerPath { get; set; }
    public string LanguageTranslationPath { get; set; }

    public static bool Exists(MCLauncherPath launcherPath)
    {
        if (launcherPath == null)
            return false;

        if (string.IsNullOrEmpty(launcherPath.MCPath))
            return false;

        if (string.IsNullOrEmpty(launcherPath.FabricInstallerPath))
            return false;

        return true;
    }
}
