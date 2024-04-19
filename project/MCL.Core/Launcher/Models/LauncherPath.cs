using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Models;

public class LauncherPath
{
    public string Path { get; set; } = VFS.FromCwd(LaunchPathHelper.Path);
    public string ModPath { get; set; } = VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.ModPath);
    public string FabricPath { get; set; } = VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.FabricPath);
    public string QuiltPath { get; set; } = VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.QuiltPath);
    public string PaperPath { get; set; } = VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.PaperPath);
    public string LocalizationPath { get; set; } =
        VFS.FromCwd(SettingsService.DataPath, LaunchPathHelper.LocalizationPath);

    public LauncherPath() { }

    public LauncherPath(
        string path,
        string modPath,
        string fabricPath,
        string quiltPath,
        string paperPath,
        string localizationPath
    )
    {
        Path = VFS.FromCwd(path);
        ModPath = VFS.FromCwd(SettingsService.DataPath, modPath);
        FabricPath = VFS.FromCwd(SettingsService.DataPath, fabricPath);
        QuiltPath = VFS.FromCwd(SettingsService.DataPath, quiltPath);
        PaperPath = VFS.FromCwd(SettingsService.DataPath, paperPath);
        LocalizationPath = VFS.FromCwd(SettingsService.DataPath, localizationPath);
    }
}