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

namespace MCL.Core.MiniCommon.CommandParser.Converters;

public static class ArgumentConverter
{
    public static string? ToString(string input)
    {
        return input;
    }

    public static string ToNonNullString(string input)
    {
        return input;
    }

    public static int ToInt(string input)
    {
        _ = int.TryParse(input, out int number);
        return number;
    }
}
