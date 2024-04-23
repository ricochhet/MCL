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

using MCL.Core.MiniCommon.Enums;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Resolvers;

namespace MCL.Core.MiniCommon.Services;

public static class LocalizationService
{
    public static Localization Localization { get; private set; } = new();
    private static bool _loaded = false;

    /// <summary>
    /// Initialize the Localization service.
    /// </summary>
    public static void Init(string filepath, Language language, bool alwaysSaveNewTranslation = false)
    {
        if (!VFS.Exists(LocalizationPathResolver.LanguageFilePath(filepath, language)) || alwaysSaveNewTranslation)
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(filepath, language),
                new Localization(),
                new() { WriteIndented = true }
            );
        Localization = Json.Load<Localization>(LocalizationPathResolver.LanguageFilePath(filepath, language));
        if (Localization?.Entries != null)
            _loaded = true;
        else
            NotificationService.Error("error.readfile", LocalizationPathResolver.LanguageFilePath(filepath, language));
    }

    /// <summary>
    /// Get a translation value by the identifier.
    /// </summary>
    public static string Translate(string id)
    {
        if (!_loaded)
            return LocalizationServiceError(id);
        if (Localization.Entries.TryGetValue(id, out string value))
            return value;
        return LocalizationError(id);
    }

    /// <summary>
    /// Get and format a translation value by the identifier.
    /// </summary>
    public static string FormatTranslate(string id, params string[] _params)
    {
        if (!_loaded)
            return LocalizationServiceError(id, _params);
        if (Localization.Entries.TryGetValue(id, out string value))
        {
            if (_params.Length <= 0)
                return value;
            return string.Format(value, _params);
        }
        return LocalizationError(id, _params);
    }

    /// <summary>
    /// Localization service error (Could not load localization service).
    /// </summary>
    private static string LocalizationServiceError(string id, params string[] _params) =>
        $"LOCALIZATION_SERVICE_ERROR: {id} - {_params}";

    /// <summary>
    /// Localization error (Identifier was not found).
    /// </summary>
    private static string LocalizationError(string id, params string[] _params) =>
        $"NO_LOCALIZATION_ERROR: {id} - {_params}";
}
