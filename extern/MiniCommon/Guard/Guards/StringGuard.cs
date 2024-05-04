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
using System.Text.RegularExpressions;
using MiniCommon.Guard.Interfaces;

namespace MiniCommon.Guard.Guards;

#pragma warning disable IDE0060, RCS1175, RCS1163, RCS1158, S107

public static partial class StringGuard
{
    public static void NullOrEmpty(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrWhiteSpace(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void LeadingAndTailingSpace(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (argument.Trim() != argument)
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing leading and tailing space", argumentName)
            );
        }
    }

    public static void MinimumLength(
        this IGuardClause guardClause,
        string argument,
        string argumentName,
        int minLength
    )
    {
        if (argument.Length < minLength)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing characters less than {1}",
                    argumentName,
                    minLength
                )
            );
        }
    }

    public static void MaximumLength(
        this IGuardClause guardClause,
        string argument,
        string argumentName,
        int maxLength
    )
    {
        if (argument.Length > maxLength)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing characters more than {1}",
                    argumentName,
                    maxLength
                )
            );
        }
    }

    public static void SpecialCharacters(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (HasSpecialChars(argument))
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing any special characters", argumentName)
            );
        }
    }

    public static void Digits(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (HasDigits(argument))
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing any digits", argumentName)
            );
        }
    }

    public static void Alphabet(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (HasAlphabets(argument))
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing any alphabets", argumentName)
            );
        }
    }

    public static void LowerCase(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (HasLowerCase(argument))
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing lower case", argumentName)
            );
        }
    }

    public static void UpperCase(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        if (HasUpperCase(argument))
        {
            throw new ArgumentException(
                string.Format("{0} is not allowing upper case", argumentName)
            );
        }
    }

    public static void Space(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (argument.Contains(' '))
        {
            throw new ArgumentException(string.Format("{0} is not allowing space", argumentName));
        }
    }

    private static bool HasSpecialChars(string value)
    {
        Regex rx = HasSpecialCharsRegex();
        return rx.IsMatch(value);
    }

    private static bool HasDigits(string value)
    {
        Regex rx = HasDigitsRegex();
        return rx.IsMatch(value);
    }

    private static bool HasAlphabets(string value)
    {
        Regex rx = HasAlphabetsRegex();
        return rx.IsMatch(value);
    }

    private static bool HasUpperCase(string value)
    {
        Regex rx = HasUpperCaseRegex();
        return rx.IsMatch(value);
    }

    private static bool HasLowerCase(string value)
    {
        Regex rx = HasLowerCaseRegex();
        return rx.IsMatch(value);
    }

    [GeneratedRegex(@"[~`!@#$%^&*()-+=|\{}':;.,<>/?]")]
    private static partial Regex HasSpecialCharsRegex();

    [GeneratedRegex(@"[0-9]")]
    private static partial Regex HasDigitsRegex();

    [GeneratedRegex(@"[a-zA-Z]")]
    private static partial Regex HasAlphabetsRegex();

    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex HasUpperCaseRegex();

    [GeneratedRegex(@"[a-z]")]
    private static partial Regex HasLowerCaseRegex();
}
