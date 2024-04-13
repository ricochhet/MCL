using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Resolvers.SevenZip;

public static class SevenZipPathResolver
{
    public static string SevenZipPath(SevenZipConfig sevenZipConfig)
    {
        string windowsExecutable = VFS.Combine(
            ConfigService.DataPath,
            "SevenZip",
            sevenZipConfig.SevenZipExecutable + ".exe"
        );
        if (VFS.Exists(windowsExecutable))
            return windowsExecutable;
        return sevenZipConfig.SevenZipExecutable;
    }
}
