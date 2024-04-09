using MCL.Core.Enums.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Services;

public static class LocalizationPathResolver
{
    public static string LanguageFilePath(MCLauncherPath launcherPath, LanguageEnum language) =>
        VFS.Combine(
            launcherPath.LanguageLocalizationPath,
            $"localization.{LanguageEnumResolver.ToString(language)}.json"
        );
}
