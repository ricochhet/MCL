using MCL.Core.MiniCommon;

namespace MCL.Core.Models.Launcher;

public class MCLauncherPath(string path, string modPath, string fabricInstallerPath, string languageLocalizationPath)
{
    public string Path { get; set; } = VFS.FromCwd(path);
    public string ModPath { get; set; } = VFS.FromCwd(modPath);
    public string FabricInstallerPath { get; set; } = VFS.FromCwd(fabricInstallerPath);
    public string LanguageLocalizationPath { get; set; } = VFS.FromCwd(languageLocalizationPath);

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
