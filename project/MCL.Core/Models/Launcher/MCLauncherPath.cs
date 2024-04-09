namespace MCL.Core.Models.Launcher;

public class MCLauncherPath
{
    public string Path { get; set; }
    public string FabricInstallerPath { get; set; }
    public string LanguageTranslationPath { get; set; }

    public MCLauncherPath(string path, string fabricInstallerPath, string languageTranslationPath)
    {
        Path = path;
        FabricInstallerPath = fabricInstallerPath;
        LanguageTranslationPath = languageTranslationPath;
    }

    public static bool Exists(MCLauncherPath launcherPath)
    {
        if (launcherPath == null)
            return false;

        if (string.IsNullOrEmpty(launcherPath.Path))
            return false;

        if (string.IsNullOrEmpty(launcherPath.FabricInstallerPath))
            return false;

        return true;
    }
}
