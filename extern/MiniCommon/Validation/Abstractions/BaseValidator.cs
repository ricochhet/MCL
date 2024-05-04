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
using MiniCommon.Logger.Enums;
using MiniCommon.Providers;
using MiniCommon.Validation.Exceptions;
using MiniCommon.Validation.Models;

namespace MiniCommon.Validation.Abstractions;

public class BaseValidator<T>
    where T : class
{
    private readonly List<ValidationRule<T>> _rules;

    public BaseValidator() => _rules = [];

    /// <summary>
    /// Add a new validation rule.
    /// </summary>
    public void AddRule(Func<T?, bool> rule, string errorMessage) => _rules.Add(new(rule, errorMessage));

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
    public bool Validate(T? obj, NativeLogLevel level)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        if (level == NativeLogLevel.Fatal && _errors.Count > 0)
        {
            throw new ObjectValidationException(string.Join(", ", _errors));
        }
        else
        {
            foreach (string error in _errors)
                NotificationProvider.PrintLog(level, error);
        }
        return _errors.Count == 0;
    }
}
