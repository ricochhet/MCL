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
using CodeAnalyzers.Analyzers.Models;
using MiniCommon.IO;
using MiniCommon.Logger.Enums;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace CodeAnalyzers.Analyzers;

public static partial class NamespaceAnalyzer
{
    /// <summary>
    /// Analyze all files, compare the namespace with the files relative directory, and repairs it to match.
    /// </summary>
    public static void Analyze(string[] files, string baseNamespace)
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

            if (Validate.For.IsNullOrWhiteSpace([name], NativeLogLevel.Debug))
            {
                NotificationProvider.Error(
                    "analyzer.error.namespace",
                    file,
                    name ?? Validate.For.EmptyString()
                );
                continue;
            }

            string path = name.Replace("\\", "/")
                .Replace(";", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);
            string _baseNamespace = baseNamespace
                .Replace("\\", "/")
                .Replace("../", string.Empty)
                .Replace(".", "/")
                .Replace(" ", string.Empty);
            string directory =
                _baseNamespace
                + VFS.GetDirectoryName(file)
                    .Replace("\\", "/")
                    .Replace("../", string.Empty)
                    .Replace(".", "/")
                    .Replace(" ", string.Empty)
                    .Split(_baseNamespace)[^1];

            if (path == directory)
            {
                success++;
            }
            else
            {
                string oldNamespace = "namespace " + path.Replace("/", ".") + ";";
                string newNamespace = "namespace " + directory.Replace("/", ".") + ";";

                fail++;
                NotificationProvider.Error(
                    "analyzer.error.namespace",
                    VFS.GetFileName(file),
                    oldNamespace,
                    newNamespace
                );
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
