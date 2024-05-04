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

using System.Linq;
using MiniCommon.Enums;
using MiniCommon.IO;
using MiniCommon.Models;
using MiniCommon.Resolvers;

namespace MiniCommon.Providers;

public static class LocalizationProvider
{
    public static Localization? Localization { get; private set; } = new();
    private static bool _initialized = false;

    /// <summary>
    /// Initialize the Localization service.
    /// </summary>
    public static void Init(
        string filepath,
        Language language,
        bool alwaysSaveNewTranslation = false
    )
    {
        if (
            language != Language.ENGLISH
            && VFS.Exists(LocalizationPathResolver.LanguageFilePath(filepath, Language.ENGLISH))
        )
        {
            Localization = Json.Load<Localization>(
                LocalizationPathResolver.LanguageFilePath(filepath, Language.ENGLISH),
                LocalizationContext.Default
            );
            if (Localization?.Entries == null)
                return;
        }

        if (
            !VFS.Exists(LocalizationPathResolver.LanguageFilePath(filepath, language))
            || alwaysSaveNewTranslation
        )
        {
            Json.Save(
                LocalizationPathResolver.LanguageFilePath(filepath, language),
                new Localization(),
                LocalizationContext.Default
            );
        }
        Localization? local = Json.Load<Localization>(
            LocalizationPathResolver.LanguageFilePath(filepath, language),
            LocalizationContext.Default
        );
        if (Localization?.Entries == null || local?.Entries == null)
            return;

        Localization!.Entries = Localization
            .Entries.Concat(local.Entries)
            .GroupBy(a => a)
            .Select(a => a.Last())
            .ToDictionary();
        if (Localization!.Entries != null)
        {
            _initialized = true;
        }
        else
        {
            NotificationProvider.Error(
                "error.readfile",
                LocalizationPathResolver.LanguageFilePath(filepath, language)
            );
        }
    }

    /// <summary>
    /// Get a translation value by the identifier.
    /// </summary>
    public static string Translate(string id)
    {
        if (!_initialized)
            return LocalizationServiceError(id);
        if (Localization?.Entries.TryGetValue(id, out string? value) ?? false)
            return value;
        return LocalizationError(id);
    }

    /// <summary>
    /// Get and format a translation value by the identifier.
    /// </summary>
    public static string FormatTranslate(string id, params string[] _params)
    {
        if (!_initialized)
            return LocalizationServiceError(id, _params);
        if (Localization?.Entries.TryGetValue(id, out string? value) ?? false)
        {
            if (_params.Length == 0)
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
