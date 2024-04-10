using MCL.Core.Enums.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Services;

namespace MCL.Core.Services;

public static class LocalizationService
{
    private static Localization translation = new();

    public static void Init(MCLauncherPath launcherPath, Language language, bool alwaysSaveNewTranslation = false)
    {
        if (!VFS.Exists(LocalizationPathResolver.LanguageFilePath(launcherPath, language)) || alwaysSaveNewTranslation)
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(launcherPath, language),
                new Localization(),
                new() { WriteIndented = true }
            );
        translation = Json.Load<Localization>(LocalizationPathResolver.LanguageFilePath(launcherPath, language));
    }

    public static string Translate(string id)
    {
        foreach ((string key, string value) in translation.Entries)
        {
            if (key == id)
                return value;
        }
        return "NO_LOCALIZATION";
    }
}
