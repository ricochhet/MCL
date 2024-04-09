using System.IO;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class TranslationPathResolver
{
    public static string LanguageFilePath(MCLauncherPath launcherPath)
    {
        return VFS.Combine(launcherPath.LanguageTranslationPath, "translations.json");
    }
}
