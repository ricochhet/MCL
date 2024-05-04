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

public static class ClassValidator
{
    /// <summary>
    /// Validate object of type T is not null.
    /// </summary>
    public static bool IsNotNull<T>(
        this IValidationClause clause,
        T? obj,
        NativeLogLevel level = NativeLogLevel.Error,
        object[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string? objName = null,
        [CallerArgumentExpression(nameof(properties))] string? propertiesName = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : class =>
        !IsNull(
            clause,
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
    /// Validate object of type T is not null.
    /// </summary>
    public static bool IsNotNull<T>(
        this IValidationClause clause,
        T? obj,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        object[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string? objName = null,
        [CallerArgumentExpression(nameof(properties))] string? propertiesName = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : class =>
        !IsNull(clause, obj, message, level, properties, objName, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate object of type T is null.
    /// </summary>
    public static bool IsNull<T>(
        this IValidationClause clause,
        T? obj,
        NativeLogLevel level = NativeLogLevel.Error,
        object[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string? objName = "",
        [CallerArgumentExpression(nameof(properties))] string? propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : class =>
        IsNull(
            clause,
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
    /// Validate object of type T is null.
    /// </summary>
    public static bool IsNull<T>(
        this IValidationClause clause,
        T? obj,
        string message,
        NativeLogLevel level = NativeLogLevel.Error,
        object[]? properties = null,
        [CallerArgumentExpression(nameof(obj))] string? objName = "",
        [CallerArgumentExpression(nameof(properties))] string? propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : class
    {
        BaseValidator<T> validator = new();

        if (string.IsNullOrWhiteSpace(message))
        {
            message = LocalizationProvider.FormatTranslate(
                "error.validation.object",
                objName ?? Validate.For.EmptyString(),
                propertiesName ?? Validate.For.EmptyString(),
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        else
        {
            message = string.Format(
                message,
                objName ?? Validate.For.EmptyString(),
                propertiesName ?? Validate.For.EmptyString(),
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }

        validator.AddRule(a => obj != default(T), message);
        foreach (object property in properties ?? [.. Validate.For.EmptyList<object>()])
            validator.AddRule(a => property != null, message);

        return !validator.Validate(obj, level);
    }
}
