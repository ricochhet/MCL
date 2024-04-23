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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon;

#pragma warning disable IDE0079
#pragma warning disable S107

public class ObjectValidator<T>
{
    private readonly List<ValidationRule<T>> _rules;

    public ObjectValidator() => _rules = [];

    /// <summary>
    /// Add a new validation rule.
    /// </summary>
    public void AddRule(Func<T, bool> rule, string errorMessage) => _rules.Add(new(rule, errorMessage));

    /// <summary>
    /// Validate object of type T.
    /// </summary>
    public bool Validate(T obj, out List<string> errors)
    {
        errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        return errors.Count == 0;
    }

    /// <summary>
    /// Validate object of type T.
    /// </summary>
    public bool Validate(T obj, Action<List<string>> action)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        action(_errors);
        return _errors.Count == 0;
    }

    /// <summary>
    /// Validate object of type T, and automatically output errors.
    /// </summary>
    public bool Validate(T obj, NativeLogLevel level)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        foreach (string error in _errors)
            NotificationService.PrintLog(level, error);
        return _errors.Count == 0;
    }

    /// <summary>
    /// Validate an array of strings is not null, empty, or whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        string[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrWhiteSpace(properties, level, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate an array of strings is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(
        string[] properties,
        NativeLogLevel level = NativeLogLevel.Error,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        foreach (string property in properties ?? [])
            validator.AddRule(
                a => !string.IsNullOrWhiteSpace(property),
                LocalizationService.FormatTranslate(
                    "error.validation.string",
                    propertiesName,
                    memberName,
                    sourceFilePath,
                    sourceLineNumber.ToString()
                )
            );

        return !validator.Validate(default, level);
    }

    /// <summary>
    /// Validate a list is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty(
        List<T> obj,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T>[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrEmpty(obj, level, properties, objName, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate a dictionary is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty<T1, T2>(
        Dictionary<T1, T2> obj,
        NativeLogLevel level = NativeLogLevel.Error,
        Dictionary<T1, T2>[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrEmpty(obj, level, properties, objName, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate a list is null, or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T1>(
        List<T1> obj,
        NativeLogLevel level = NativeLogLevel.Error,
        List<T1>[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<List<T1>> validator = new();

        string message = LocalizationService.FormatTranslate(
            "error.validation.list",
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        validator.AddRule(a => obj != null && obj.Count > 0, message);
        foreach (List<T1> property in properties ?? [])
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(obj, level);
    }

    /// <summary>
    /// Validate a dictionary is null, or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T1, T2>(
        Dictionary<T1, T2> obj,
        NativeLogLevel level = NativeLogLevel.Error,
        Dictionary<T1, T2>[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<Dictionary<T1, T2>> validator = new();

        string message = LocalizationService.FormatTranslate(
            "error.validation.list",
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        validator.AddRule(a => obj != null && obj.Count > 0, message);
        foreach (Dictionary<T1, T2> property in properties ?? [])
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(obj, level);
    }

    /// <summary>
    /// Validate object of type T is not null.
    /// </summary>
    public static bool IsNotNull(
        T obj,
        NativeLogLevel level = NativeLogLevel.Error,
        object[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = null,
        [CallerArgumentExpression(nameof(properties))] string propertiesName = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNull(obj, level, properties, objName, propertiesName, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate object of type T is null.
    /// </summary>
    public static bool IsNull(
        T obj,
        NativeLogLevel level = NativeLogLevel.Error,
        object[] properties = null,
        [CallerArgumentExpression(nameof(obj))] string objName = "",
        [CallerArgumentExpression(nameof(properties))] string propertiesName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        string message = LocalizationService.FormatTranslate(
            "error.validation.object",
            objName,
            propertiesName,
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        validator.AddRule(a => obj != null, message);
        foreach (object property in properties ?? [])
            validator.AddRule(a => property != null, message);

        return !validator.Validate(obj, level);
    }
}
