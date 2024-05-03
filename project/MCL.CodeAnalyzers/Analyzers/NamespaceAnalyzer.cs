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

using System.Text.RegularExpressions;
using MCL.CodeAnalyzers.Analyzers.Models;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.MiniCommon.Validation.Operators;
using MCL.Core.MiniCommon.Validation.Validators;

namespace MCL.CodeAnalyzers.Analyzers;

public static partial class NamespaceAnalyzer
{
    /// <summary>
    /// Analyze all files, compare the namespace with the files relative directory, and repairs it to match.
    /// </summary>
    public static void Analyze(string[] files)
    {
        int success = 0;
        int fail = 0;

        foreach (string file in files)
        {
            string fileData = VFS.ReadAllText(file);
            Regex matchNamespace = NamespaceRegex();
            Match namespaceMatch = matchNamespace.Match(fileData);
            string name = namespaceMatch.Value;

            if (AnalyzerFiles.Restricted.Exists(file.Contains))
                continue;

            if (StringValidator.IsNullOrWhiteSpace([name], NativeLogLevel.Debug))
            {
                NotificationProvider.Error("analyzer.error.namespace", file, name ?? StringOperator.Empty());
                continue;
            }

            string path = name.Replace("\\", "/")
                .Replace(";", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);
            string directory = VFS.GetDirectoryName(file)
                .Replace("\\", "/")
                .Replace("../", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);

            if (path == directory)
            {
                success++;
            }
            else
            {
                string oldNamespace = "namespace " + path.Replace("/", ".") + ";";
                string newNamespace = "namespace " + directory.Replace("/", ".") + ";";
                VFS.WriteFile(file, fileData.Replace(oldNamespace, newNamespace));

                fail++;
                NotificationProvider.Error("analyzer.error.namespace", VFS.GetFileName(file), name);
            }
        }

        NotificationProvider.Info(
            "analyzer.output",
            nameof(NamespaceAnalyzer),
            success.ToString(),
            fail.ToString(),
            (success + fail).ToString()
        );
    }

    [GeneratedRegex(@"(?<=namespace).*?;")]
    private static partial Regex NamespaceRegex();
}
