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
using MCL.CodeAnalyzers.Analyzers.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;

namespace MCL.CodeAnalyzers.Analyzers;

public static class LineAnalyzer
{
    public static void Analyze(string[] files)
    {
        List<int> fileLines = [];
        foreach (string file in files)
        {
            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;
            string[] lines = VFS.ReadAllLines(file);
            int fileLineCount = 0;
            foreach (
                string line in lines.Where(a =>
                    ObjectValidator<string>.IsNotNullOrWhiteSpace([a], NativeLogLevel.Debug)
                )
            )
                fileLineCount++;

            fileLines.Add(fileLineCount);
        }

        NotificationService.Info(
            "analyzer.line.output",
            nameof(LineAnalyzer),
            fileLines.Sum().ToString(),
            Math.Round(fileLines.Average()).ToString()
        );
    }
}
