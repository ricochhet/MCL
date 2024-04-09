namespace MCL.Core.Models.Launcher;

public class MCLauncherPath(string path, string fabricInstallerPath, string languageLocalizationPath)
{
    public string Path { get; set; } = path;
    public string FabricInstallerPath { get; set; } = fabricInstallerPath;
    public string LanguageLocalizationPath { get; set; } = languageLocalizationPath;

    public static bool Exists(MCLauncherPath launcherPath)
    {
        if (launcherPath == null)
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.Path))
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.FabricInstallerPath))
            return false;

        return true;
    }
}
