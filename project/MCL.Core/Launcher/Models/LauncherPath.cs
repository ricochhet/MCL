using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Models;

public class LauncherPath
{
    public string Path { get; set; } = VFS.FromCwd(LaunchPathHelper.Path);
    public string ModPath { get; set; } = VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.ModPath);
    public string FabricInstallerPath { get; set; } =
        VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.FabricInstallerPath);
    public string QuiltInstallerPath { get; set; } =
        VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.QuiltInstallerPath);
    public string PaperInstallerPath { get; set; } =
        VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.PaperInstallerPath);
    public string LanguageLocalizationPath { get; set; } =
        VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.LanguageLocalizationPath);

    public LauncherPath() { }

    public LauncherPath(
        string path,
        string modPath,
        string fabricInstallerPath,
        string quiltInstallerPath,
        string paperInstallerPath,
        string languageLocalizationPath
    )
    {
        Path = VFS.FromCwd(path);
        ModPath = VFS.FromCwd(SettingsService.DataPath, modPath);
        FabricInstallerPath = VFS.FromCwd(SettingsService.DataPath, fabricInstallerPath);
        QuiltInstallerPath = VFS.FromCwd(SettingsService.DataPath, quiltInstallerPath);
        PaperInstallerPath = VFS.FromCwd(SettingsService.DataPath, paperInstallerPath);
        LanguageLocalizationPath = VFS.FromCwd(SettingsService.DataPath, languageLocalizationPath);
    }
}
