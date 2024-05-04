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
using MiniCommon.Validation.Interfaces;
using MiniCommon.Validation.Operators;

namespace MiniCommon.Validation.Validators;

#pragma warning disable IDE0060, RCS1175, RCS1163, RCS1158, S107

public static class StringValidator
{
    /// <summary>
    /// Validate an array of strings is not null, empty, or whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        this IValidationClause clause,
        string?[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        !IsNullOrWhiteSpace(
            clause,
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
        this IValidationClause clause,
        string?[] properties,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrWhiteSpace(clause, properties, message, level, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate an array of strings is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(
        this IValidationClause clause,
        string?[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        IsNullOrWhiteSpace(
            clause,
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
        this IValidationClause clause,
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

        foreach (string? property in properties ?? [.. Validate.For.EmptyList<string>()])
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
