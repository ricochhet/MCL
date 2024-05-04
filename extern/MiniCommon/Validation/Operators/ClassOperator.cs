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
using MiniCommon.Validation.Interfaces;

#pragma warning disable IDE0060, RCS1175, RCS1163, RCS1158, S107

namespace MiniCommon.Validation.Operators;

public static class ClassOperator
{
    /// <summary>
    /// Coalescing operator shim for empty class to log when it gets called.
    /// </summary>
    public static T EmptyClass<T>(
        this IValidationClause clause,
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : new() =>
        EmptyClass<T>(clause, string.Empty, level, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Coalescing operator shim for empty class to log when it gets called.
    /// </summary>
    public static T EmptyClass<T>(
        this IValidationClause clause,
        string message,
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : new()
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            NotificationProvider.Log(
                level,
                "error.validation.class-shim",
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        else
        {
            NotificationProvider.PrintLog(
                level,
                message,
                memberName,
                sourceFilePath,
                sourceLineNumber.ToString()
            );
        }
        return new();
    }
}
