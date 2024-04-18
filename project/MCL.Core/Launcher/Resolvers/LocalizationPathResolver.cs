using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Resolvers;

public static class LocalizationPathResolver
{
    public static string LanguageFilePath(LauncherPath launcherPath, Language language) =>
        VFS.FromCwd(launcherPath.LocalizationPath, $"localization.{LanguageResolver.ToString(language)}.json");
}
