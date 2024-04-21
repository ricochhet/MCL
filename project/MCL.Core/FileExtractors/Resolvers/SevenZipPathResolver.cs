using MCL.Core.FileExtractors.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.FileExtractors.Resolvers;

public static class SevenZipPathResolver
{
    public static string SevenZipPath(SevenZipSettings sevenZipSettings)
    {
        string windowsExecutable = VFS.Combine(
            SettingsService.DataPath,
            "SevenZip",
            sevenZipSettings.Executable + ".exe"
        );
        if (VFS.Exists(windowsExecutable))
            return windowsExecutable;
        NotificationService.Info("error.missing.7z");
        return sevenZipSettings.Executable;
    }
}
