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
using MCL.Core.MiniCommon.Validation.Abstractions;
using MCL.Core.MiniCommon.Validation.Operators;

namespace MCL.Core.MiniCommon.Validation.Validators;

#pragma warning disable RCS1163, RCS1158, S107

public static class ListValidator
{
    /// <summary>
    /// Coalescing operator shim for empty list to log when it gets called.
    /// </summary>
    public static List<T> ListEmpty<T>(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => ListEmpty<T>(string.Empty, level, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Coalescing operator shim for empty list to log when it gets called.
    /// </summary>
    public static List<T> ListEmpty<T>(
        string message,
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            NotificationProvider.Log(
                level,
                "error.validation.list-shim",
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        else
        {
            NotificationProvider.PrintLog(level, message, memberName, sourceFilePath, sourceLineNumber.ToString());
        }
        return [];
    }

    /// <summary>
    /// Validate a list is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty<T>(
        List<T>? obj,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T>[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        !IsNullOrEmpty(
            obj,
            string.Empty,
            level,
            properties,
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber
        );

    /// <summary>
    /// Validate a list is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty<T>(
        List<T>? obj,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T>[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        !IsNullOrEmpty(
            obj,
            message,
            level,
            properties,
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber
        );

    /// <summary>
    /// Validate a list is null, or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(
        List<T>? obj,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T>[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        IsNullOrEmpty(
            obj,
            string.Empty,
            level,
            properties,
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber
        );

    /// <summary>
    /// Validate a list is null, or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(
        List<T>? obj,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T>[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        BaseValidator<List<T>> validator = new();

        if (string.IsNullOrWhiteSpace(message))
        {
            message = LocalizationProvider.FormatTranslate(
                "error.validation.list",
                objName,
                propertiesName,
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        else
        {
            message = string.Format(
                message,
                objName ?? StringOperator.Empty(),
                propertiesName ?? StringOperator.Empty(),
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        validator.AddRule(a => obj?.Count > 0, message);
        foreach (List<T> property in properties ?? [.. ListOperator.Empty<List<T>>()])
            validator.AddRule(a => property?.Count > 0, message);

        return !validator.Validate(obj, level);
    }
}
