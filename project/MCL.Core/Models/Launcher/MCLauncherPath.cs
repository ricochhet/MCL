using MCL.Core.MiniCommon;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Models.Launcher;

public class MCLauncherPath(
    string path,
    string modPath,
    string fabricInstallerPath,
    string quiltInstallerPath,
    string languageLocalizationPath
)
{
    public string Path { get; set; } = VFS.FromCwd(path);
    public string ModPath { get; set; } = VFS.FromCwd(ConfigService.DataPath, modPath);
    public string FabricInstallerPath { get; set; } = VFS.FromCwd(ConfigService.DataPath, fabricInstallerPath);
    public string QuiltInstallerPath { get; set; } = VFS.FromCwd(ConfigService.DataPath, quiltInstallerPath);
    public string LanguageLocalizationPath { get; set; } =
        VFS.FromCwd(ConfigService.DataPath, languageLocalizationPath);

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
