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
using MiniCommon.Guard.Interfaces;

namespace MiniCommon.Guard.Guards;

#pragma warning disable IDE0060, RCS1175, RCS1163, RCS1158, S107

public static class TimeSpanGuard
{
    public static void TimeSpanLessThan(
        this IGuardClause guardClause,
        TimeSpan argument,
        string argumentName,
        TimeSpan min,
        string minArgumentName = "",
        string format = @"hh\:mm\:ss"
    )
    {
        if (argument < min)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing less than {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(minArgumentName)
                        ? minArgumentName
                        : min.ToString(format)
                )
            );
        }
    }

    public static void TimeSpanGreaterThan(
        this IGuardClause guardClause,
        TimeSpan argument,
        string argumentName,
        TimeSpan max,
        string maxArgumentName = "",
        string format = @"hh\:mm\:ss"
    )
    {
        if (argument > max)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing more than {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(maxArgumentName)
                        ? maxArgumentName
                        : max.ToString(format)
                )
            );
        }
    }

    public static void TimeSpanLessThanOrEqual(
        this IGuardClause guardClause,
        TimeSpan argument,
        string argumentName,
        TimeSpan min,
        string minArgumentName = "",
        string format = @"hh\:mm\:ss"
    )
    {
        if (argument <= min)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing less than or equals to {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(minArgumentName)
                        ? minArgumentName
                        : min.ToString(format)
                )
            );
        }
    }

    public static void TimeSpanGreaterThanOrEqual(
        this IGuardClause guardClause,
        TimeSpan argument,
        string argumentName,
        TimeSpan max,
        string maxArgumentName = "",
        string format = @"hh\:mm\:ss"
    )
    {
        if (argument >= max)
        {
            throw new ArgumentException(
                string.Format(
                    "{0} is not allowing greater than or equals to {1}",
                    argumentName,
                    !string.IsNullOrWhiteSpace(maxArgumentName)
                        ? maxArgumentName
                        : max.ToString(format)
                )
            );
        }
    }

    public static void TimeSpanOutOfRange(
        this IGuardClause guardClause,
        TimeSpan argument,
        string argumentName,
        TimeSpan startRange,
        TimeSpan endRange
    )
    {
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
}
