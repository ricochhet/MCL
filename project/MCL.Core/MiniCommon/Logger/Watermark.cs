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

using System.Collections.Generic;
using System.Linq;
using MCL.Core.MiniCommon.Providers;

namespace MCL.Core.MiniCommon.Logger;

public static class Watermark
{
    /// <summary>
    /// Write an array of text to stdout with box ASCII around it.
    /// </summary>
    public static void Draw(List<string> text)
    {
        List<string> result = [];
        int longestLength = text.Max(TextLength);
        string line = new('-', longestLength);
        result.Add($"┌─{line}─┐");

        foreach (string textItem in text)
        {
            int spacingSize = longestLength - TextLength(textItem);
            string spacingText = textItem + new string(' ', spacingSize);
            result.Add($"│ {spacingText} │");
        }

        result.Add($"└─{line}─┘");
        foreach (string textItem in result)
            NotificationProvider.InfoLog(textItem);
    }

    private static int TextLength(string s)
    {
        return s.Replace("[\u0391-\uFFE5]", "ab").Length;
    }
}
