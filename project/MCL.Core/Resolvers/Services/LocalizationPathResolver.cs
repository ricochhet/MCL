using MCL.Core.Enums.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class LocalizationPathResolver
{
    public static string LanguageFilePath(LauncherPath launcherPath, Language language) =>
        VFS.FromCwd(launcherPath.LanguageLocalizationPath, $"localization.{LanguageResolver.ToString(language)}.json");
}
