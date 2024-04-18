using MCL.Core.Enums.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Services;

namespace MCL.Core.Services.Launcher;

public static class LocalizationService
{
    public static Localization Localization { get; private set; } = new();
    private static bool Loaded = false;

    public static void Init(MCLauncherPath launcherPath, Language language, bool alwaysSaveNewTranslation = false)
    {
        if (!VFS.Exists(LocalizationPathResolver.LanguageFilePath(launcherPath, language)) || alwaysSaveNewTranslation)
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(launcherPath, language),
                new Localization(),
                new() { WriteIndented = true }
            );
        Localization = Json.Load<Localization>(LocalizationPathResolver.LanguageFilePath(launcherPath, language));
        if (Localization?.Entries != null)
            Loaded = true;
        else
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.readfile",
                [LocalizationPathResolver.LanguageFilePath(launcherPath, language)]
            );
    }

    public static string Translate(string id)
    {
        if (!Loaded)
            return $"{id}:LOCALIZATION_SERVICE_ERROR";

        if (Localization.Entries.TryGetValue(id, out string value))
            return value;
        return $"{id}:NO_LOCALIZATION";
    }
}
