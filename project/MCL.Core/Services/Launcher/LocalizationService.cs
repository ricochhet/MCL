using MCL.Core.Enums.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Resolvers.Services;

namespace MCL.Core.Services.Launcher;

public static class LocalizationService
{
    private static Localization translation = new();
    private static bool Loaded = false;

    public static void Init(MCLauncherPath launcherPath, Language language, bool alwaysSaveNewTranslation = false)
    {
        if (!VFS.Exists(LocalizationPathResolver.LanguageFilePath(launcherPath, language)) || alwaysSaveNewTranslation)
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(launcherPath, language),
                new Localization(),
                new() { WriteIndented = true }
            );
        translation = Json.Load<Localization>(LocalizationPathResolver.LanguageFilePath(launcherPath, language));
        if (translation?.Entries != null)
            Loaded = true;
        else
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "error.readfile",
                    [LocalizationPathResolver.LanguageFilePath(launcherPath, language)]
                )
            );
    }

    public static string Translate(string id)
    {
        if (!Loaded)
            return "LOCALIZATION_SERVICE_ERROR";

        if (translation.Entries.TryGetValue(id, out string value))
            return value;
        return "NO_LOCALIZATION";
    }
}
