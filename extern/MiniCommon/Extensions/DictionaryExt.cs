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

namespace MiniCommon.Extensions;

public static class DictionaryExt
{
    private static readonly char[] _seperator = [',', ';'];

    /// <summary>
    /// Parses a string containing key-value pairs separated by specified separators into a dictionary.
    /// </summary>
    public static Dictionary<string, string> ParseKeyValuePairs(this string input)
    {
        Dictionary<string, string> keyValuePairs = [];
        string[] pairs = input.Split(_seperator, StringSplitOptions.RemoveEmptyEntries);
        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split('=');
            if (keyValue.Length == 2)
                keyValuePairs[keyValue[0]] = keyValue[1];
        }
        return keyValuePairs;
    }
}
