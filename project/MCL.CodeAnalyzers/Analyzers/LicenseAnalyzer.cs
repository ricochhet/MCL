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

using MCL.CodeAnalyzers.Analyzers.Models;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Providers;

namespace MCL.CodeAnalyzers.Analyzers;

public static class LicenseAnalyzer
{
    /// <summary>
    /// Analyze all files, and search for an existing LICENSE header, specified by LICENSE-NOTICE.txt.
    /// </summary>
    public static void Analyze(string[] files)
    {
        int success = 0;
        int fail = 0;

        string license = VFS.ReadAllText(VFS.FromCwd("LICENSE-NOTICE.txt"))
            .Replace("\n", string.Empty)
            .Replace("\r", string.Empty);

        foreach (string file in files)
        {
            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;
            string fileData = VFS.ReadAllText(file).Replace("\n", string.Empty).Replace("\r", string.Empty);
            if (!fileData.Contains(license))
            {
                fail++;
                NotificationProvider.Error("analyzer.error.license", file);
            }
            else
            {
                success++;
            }
        }

        NotificationProvider.Info(
            "analyzer.output",
            nameof(LicenseAnalyzer),
            success.ToString(),
            fail.ToString(),
            (success + fail).ToString()
        );
    }
}
