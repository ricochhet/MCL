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

public static partial class StringDataGuard
{
    public static void InValidURL(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidURL(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid URL", argumentName));
        }
    }

    public static void InValidEmailId(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidEmailId(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid emailid", argumentName));
        }
    }

    public static void InValidGuid(
        this IGuardClause guardClause,
        string argument,
        string argumentName
    )
    {
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidGuid(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid Guid", argumentName));
        }
    }

    private static bool IsValidURL(string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out Uri? result)
            && (result.Scheme == "http" || result.Scheme == "https");
    }

    private static bool IsValidEmailId(string value)
    {
        return ValidEmailRegex().IsMatch(value);
    }

    private static bool IsValidGuid(string value)
    {
        return Guid.TryParse(value, out Guid _);
    }

    [GeneratedRegex(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$"
    )]
    private static partial Regex ValidEmailRegex();
}
