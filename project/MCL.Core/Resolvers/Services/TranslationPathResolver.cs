using System.IO;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class TranslationPathResolver
{
    public static string LanguageFilePath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.LanguageTranslationPath, "translations.json");
    }
}
