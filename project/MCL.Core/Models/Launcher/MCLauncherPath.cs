using MCL.Core.Helpers.Launcher;
using MCL.Core.MiniCommon;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Models.Launcher;

public class MCLauncherPath
{
    public string Path { get; set; } = VFS.FromCwd(LaunchPathHelper.Path);
    public string ModPath { get; set; } = VFS.FromCwd(ConfigService.DataPath, LaunchPathHelper.ModPath);
    public string FabricInstallerPath { get; set; } =
        VFS.FromCwd(ConfigService.DataPath, LaunchPathHelper.FabricInstallerPath);
    public string QuiltInstallerPath { get; set; } =
        VFS.FromCwd(ConfigService.DataPath, LaunchPathHelper.QuiltInstallerPath);
    public string PaperInstallerPath { get; set; } =
        VFS.FromCwd(ConfigService.DataPath, LaunchPathHelper.PaperInstallerPath);
    public string LanguageLocalizationPath { get; set; } =
        VFS.FromCwd(ConfigService.DataPath, LaunchPathHelper.LanguageLocalizationPath);

    public MCLauncherPath() { }

    public MCLauncherPath(
        string path,
        string modPath,
        string fabricInstallerPath,
        string quiltInstallerPath,
        string paperInstallerPath,
        string languageLocalizationPath
    )
    {
        Path = VFS.FromCwd(path);
        ModPath = VFS.FromCwd(ConfigService.DataPath, modPath);
        FabricInstallerPath = VFS.FromCwd(ConfigService.DataPath, fabricInstallerPath);
        QuiltInstallerPath = VFS.FromCwd(ConfigService.DataPath, quiltInstallerPath);
        PaperInstallerPath = VFS.FromCwd(ConfigService.DataPath, paperInstallerPath);
        LanguageLocalizationPath = VFS.FromCwd(ConfigService.DataPath, languageLocalizationPath);
    }

    public static bool Exists(MCLauncherPath launcherPath)
    {
        if (launcherPath == null)
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.Path))
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.FabricInstallerPath))
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.QuiltInstallerPath))
            return false;

        if (string.IsNullOrWhiteSpace(launcherPath.PaperInstallerPath))
            return false;

        return true;
    }
}
