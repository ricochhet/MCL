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
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Commands;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Modding.Resolvers;
using MCL.Core.Modding.Services;

namespace MCL.Launcher.Commands.Modding;

public class DeployMods : ILauncherCommand
{
    public Task Init(string[] args, Settings? settings)
    {
        CommandLine.ProcessArgument(
            args,
            new() { Name = "deploy-mods" },
            (string? value) =>
            {
                ModdingService.Save(value);
                ModdingService.Deploy(
                    ModdingService.Load(value),
                    ModPathResolver.ModDeployPath(settings?.LauncherPath)
                );
                settings?.Save(ModdingService.ModSettings);
            }
        );

        return Task.CompletedTask;
    }
}
