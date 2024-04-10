using MCL.Core.Enums.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class LocalizationPathResolver
{
    public static string LanguageFilePath(MCLauncherPath launcherPath, Language language) =>
        VFS.Combine(launcherPath.LanguageLocalizationPath, $"localization.{LanguageResolver.ToString(language)}.json");
}
