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
using System.Threading.Tasks;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.CommandParser.Converters;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.Servers.Paper.Helpers;

namespace MCL.Core.Commands.Launcher;

public class LaunchPaperServer : ILauncherCommand
{
    public Task Init(string[] args, Settings? settings)
    {
        CommandLine.ProcessArgument(
            args,
            new()
            {
                Name = "launch-paper",
                Parameters =
                [
                    new() { Name = "gameversion", Optional = true },
                    new() { Name = "paperversion", Optional = true },
                    new() { Name = "javapath", Optional = true }
                ],
                Description = LocalizationProvider.Translate("command.launch-paper-server")
            },
            options =>
            {
                if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
                    return;

                settings!.Set(
                    options,
                    "gameversion",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherVersion?.MVersion,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherVersion>.IsNull(s?.LauncherVersion))
                            return;
                        s!.LauncherVersion!.MVersion = v;
                    }
                );
                settings!.Set(
                    options,
                    "paperversion",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherVersion?.PaperServerVersion,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherVersion>.IsNull(s?.LauncherVersion))
                            return;
                        s!.LauncherVersion!.PaperServerVersion = v;
                    }
                );
                PaperLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
            }
        );

        return Task.CompletedTask;
    }
}
