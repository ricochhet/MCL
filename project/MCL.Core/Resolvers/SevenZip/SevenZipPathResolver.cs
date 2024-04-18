using MCL.Core.MiniCommon;
using MCL.Core.Models.SevenZip;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Resolvers.SevenZip;

public static class SevenZipPathResolver
{
    public static string SevenZipPath(SevenZipSettings sevenZipSettings)
    {
        string windowsExecutable = VFS.Combine(
            SettingsService.DataPath,
            "SevenZip",
            sevenZipSettings.SevenZipExecutable + ".exe"
        );
        if (VFS.Exists(windowsExecutable))
            return windowsExecutable;
        return sevenZipSettings.SevenZipExecutable;
    }
}
