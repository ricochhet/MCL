namespace MCL.Core.Models.Launcher;

public class MCLauncherPath(string path, string fabricInstallerPath, string languageTranslationPath)
{
    public string Path { get; set; } = path;
    public string FabricInstallerPath { get; set; } = fabricInstallerPath;
    public string LanguageTranslationPath { get; set; } = languageTranslationPath;

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
