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

using System.Runtime.CompilerServices;
using MiniCommon.Logger.Enums;
using MiniCommon.Providers;
using MiniCommon.Validation.Abstractions;
using MiniCommon.Validation.Operators;

namespace MiniCommon.Validation.Validators;

#pragma warning disable RCS1163, RCS1158, S107

public static class StringValidator
{
    /// <summary>
    /// Coalescing operator shim for string.Empty to log when it gets called.
    /// </summary>
    public static string StringEmpty(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => StringEmpty(string.Empty, level, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Coalescing operator shim for string.Empty to log when it gets called.
    /// </summary>
    public static string StringEmpty(
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
                "error.validation.string-shim",
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        else
        {
            NotificationProvider.PrintLog(level, message, memberName, sourceFilePath, sourceLineNumber.ToString());
        }
        return string.Empty;
    }

    /// <summary>
    /// Validate an array of strings is not null, empty, or whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        string?[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        !IsNullOrWhiteSpace(
            properties,
            string.Empty,
            level,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber
        );

    /// <summary>
    /// Validate an array of strings is not null, empty, or whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        string?[] properties,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrWhiteSpace(properties, message, level, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate an array of strings is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(
        string?[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        IsNullOrWhiteSpace(
            properties,
            string.Empty,
            level,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber
        );

    /// <summary>
    /// Validate an array of strings is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(
        string?[] properties,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        BaseValidator<string> validator = new();

        foreach (string? property in properties ?? [.. ListOperator.Empty<string>()])
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = LocalizationProvider.FormatTranslate(
                    "error.validation.string",
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
                    propertiesName,
                    memberName,
                    sourceFilePath,
                    sourceLineNumber.ToString()
                );
            }

            validator.AddRule(a => !string.IsNullOrWhiteSpace(property), message);
        }

        return !validator.Validate(default, level);
    }
}
