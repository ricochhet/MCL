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
using MCL.CodeAnalyzers.Analyzers;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation.Operators;

namespace MCL.CodeAnalyzers.Commands;

public class AnalyzeCode : IBaseCommand
{
    public Task Init(string[] args, Settings? settings)
    {
        CommandLine.ProcessArgument(
            args,
            new() { Name = "analyze", Parameters = [new() { Name = "path", Optional = false }] },
            options =>
            {
                string[] files = VFS.GetFiles(
                    options.GetValueOrDefault("path", "./"),
                    "*.cs",
                    SearchOption.AllDirectories
                );

                LicenseAnalyzer.Analyze(files);
                NamespaceAnalyzer.Analyze(files);
                LocalizationKeyAnalyzer.Analyze(
                    files,
                    LocalizationProvider.Localization ?? ClassOperator.Empty<Localization>()
                );
            }
        );

        return Task.CompletedTask;
    }
}
