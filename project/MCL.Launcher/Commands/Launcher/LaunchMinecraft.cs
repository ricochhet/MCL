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
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Resolvers;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchMinecraft : ILauncherCommand
{
    public Task Init(string[] args, Settings settings)
    {
        CommandLine.ProcessArgument(
            args,
            new()
            {
                Name = "launch",
                Parameters =
                [
                    new() { Name = "client", Optional = false },
                    new() { Name = "gameversion", Optional = true },
                    new() { Name = "fabricversion", Optional = true },
                    new() { Name = "quiltversion", Optional = true },
                    new() { Name = "username", Optional = true },
                    new() { Name = "javapath", Optional = true }
                ]
            },
            options =>
            {
                settings.LauncherSettings.ClientType = EnumResolver.Parse(
                    options.GetValueOrDefault("client", "vanilla"),
                    ClientType.VANILLA
                );
                settings.Set(
                    options,
                    "gameversion",
                    s => s.LauncherVersion.MVersion,
                    (s, v) => s.LauncherVersion.MVersion = v
                );
                settings.Set(
                    options,
                    "fabricversion",
                    s => s.LauncherVersion.FabricLoaderVersion,
                    (s, v) => s.LauncherVersion.FabricLoaderVersion = v
                );
                settings.Set(
                    options,
                    "quiltversion",
                    s => s.LauncherVersion.QuiltLoaderVersion,
                    (s, v) => s.LauncherVersion.QuiltLoaderVersion = v
                );
                settings.Set(
                    options,
                    "username",
                    s => s.LauncherUsername.Username,
                    (s, v) => s.LauncherUsername.Username = v
                );
                MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
            }
        );

        return Task.CompletedTask;
    }
}
