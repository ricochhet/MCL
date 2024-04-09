using MCL.Core.Enums.Services;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Services;

namespace MCL.Core.Services;

public static class TranslationService
{
    private static Translation translation;
    private static LanguageEnum language;

    static TranslationService()
    {
        translation = new();
    }

    public static void InitService(
        MCLauncherPath launcherPath,
        LanguageEnum _language,
        bool alwaysSaveNewTranslation = false
    )
    {
        language = _language;
        if (!VFS.Exists(TranslationPathResolver.LanguageFilePath(launcherPath)) || alwaysSaveNewTranslation)
            Json.Save(
                TranslationPathResolver.LanguageFilePath(launcherPath),
                new Translation(),
                new() { WriteIndented = true }
            );
        translation = Json.Load<Translation>(TranslationPathResolver.LanguageFilePath(launcherPath));
    }

    public static string Translate(string id)
    {
        string parsedId = $"{id}.{LanguageEnumResolver.ToString(language)}";
        foreach ((string key, string value) in translation.Entries)
        {
            if (key == parsedId)
                return value;
        }
        return "NO_TRANSLATION";
    }
}
