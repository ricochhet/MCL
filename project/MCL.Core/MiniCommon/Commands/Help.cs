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

using System.Threading.Tasks;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Helpers;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon.Commands;

public class Help : ILauncherCommand
{
    public Task Init(string[] args, Settings? settings)
    {
        CommandLine.ProcessArgument(
            args,
            new() { Name = "help", },
            (string? _) =>
            {
                foreach (Command command in CommandHelper.Commands)
                {
                    NotificationService.InfoLog(command.Usage());
                }
            }
        );

        return Task.CompletedTask;
    }
}
