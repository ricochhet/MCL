using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Resolvers;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.Core.Launcher.Services;

public static class LocalizationService
{
    public static Localization Localization { get; private set; } = new();
    private static bool _loaded = false;

    public static void Init(LauncherPath launcherPath, Language language, bool alwaysSaveNewTranslation = false)
    {
        if (!VFS.Exists(LocalizationPathResolver.LanguageFilePath(launcherPath, language)) || alwaysSaveNewTranslation)
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(launcherPath, language),
                new Localization(),
                new() { WriteIndented = true }
            );
        Localization = Json.Load<Localization>(LocalizationPathResolver.LanguageFilePath(launcherPath, language));
        if (Localization?.Entries != null)
            _loaded = true;
        else
            NotificationService.Log(
                NativeLogLevel.Error,
                "error.readfile",
                LocalizationPathResolver.LanguageFilePath(launcherPath, language)
            );
    }

    public static string Translate(string id)
    {
        if (!_loaded)
            return $"{id}:LOCALIZATION_SERVICE_ERROR";

        if (Localization.Entries.TryGetValue(id, out string value))
            return value;
        return $"{id}:NO_LOCALIZATION";
    }

    public static string FormatTranslate(string id, params string[] _params)
    {
        if (!_loaded)
            return $"{id}:LOCALIZATION_SERVICE_ERROR";

        if (Localization.Entries.TryGetValue(id, out string value))
            return string.Format(value, _params);
        return $"{id}:NO_LOCALIZATION";
    }
}
