/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

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
