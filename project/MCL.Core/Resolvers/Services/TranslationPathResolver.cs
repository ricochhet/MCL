using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class TranslationPathResolver
{
    public static string LanguageFilePath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.LanguageTranslationPath, "translations.json");
}
