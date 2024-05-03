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

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Providers;

namespace MCL.Core.MiniCommon.Validation;

public static class ValidationShims
{
    /// <summary>
    /// Coalescing operator shim for string.Empty to log when it gets called.
    /// </summary>
    public static string StringEmpty(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        NotificationProvider.Log(
            level,
            "error.validation.string-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return string.Empty;
    }

    /// <summary>
    /// Coalescing operator shim for empty list to log when it gets called.
    /// </summary>
    public static List<T> ListEmpty<T>(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        NotificationProvider.Log(
            level,
            "error.validation.list-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return [];
    }

    /// <summary>
    /// Coalescing operator shim for empty dictionary to log when it gets called.
    /// </summary>
#pragma warning disable S4144
    public static Dictionary<TKey, TValue> DictionaryEmpty<TKey, TValue>(
#pragma warning restore S4144
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where TKey : notnull
    {
        NotificationProvider.Log(
            level,
            "error.validation.list-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return [];
    }

    /// <summary>
    /// Coalescing operator shim for empty class to log when it gets called.
    /// </summary>
    public static T ClassEmpty<T>(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : new()
    {
        NotificationProvider.Log(
            level,
            "error.validation.class-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return new();
    }
}
