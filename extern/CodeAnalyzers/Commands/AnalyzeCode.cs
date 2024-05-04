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
using System.IO;
using System.Threading.Tasks;
using CodeAnalyzers.Analyzers;
using MiniCommon.CommandParser;
using MiniCommon.Interfaces;
using MiniCommon.IO;
using MiniCommon.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace CodeAnalyzers.Commands;

public class AnalyzeCode<T> : IBaseCommand<T>
{
    public Task Init(string[] args, T? settings)
    {
        CommandLine.ProcessArgument(
            args,
            new()
            {
                Name = "analyze",
                Parameters =
                [
                    new() { Name = "path", Optional = false },
                    new() { Name = "license", Optional = true }
                ]
            },
            options =>
            {
                string[] files = VFS.GetFiles(
                    options.GetValueOrDefault("path", "./"),
                    "*.cs",
                    SearchOption.AllDirectories
                );
                LicenseAnalyzer.Analyze(
                    files,
                    options.GetValueOrDefault("license", "LICENSE-NOTICE.txt")
                );
                NamespaceAnalyzer.Analyze(files);
                LocalizationKeyAnalyzer.Analyze(
                    files,
                    LocalizationProvider.Localization ?? Validate.For.EmptyClass<Localization>()
                );
            }
        );

        return Task.CompletedTask;
    }
}
