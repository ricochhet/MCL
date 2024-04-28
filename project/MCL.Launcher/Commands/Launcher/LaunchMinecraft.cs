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
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.CommandParser.Converters;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Resolvers;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Launcher.Commands.Launcher;

public class LaunchMinecraft : ILauncherCommand
{
    public Task Init(string[] args, Settings? settings)
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
                if (ObjectValidator<LauncherSettings>.IsNull(settings?.LauncherSettings))
                    return;
#pragma warning disable CS8602
                settings.LauncherSettings.ClientType = EnumResolver.Parse(
                    options.GetValueOrDefault("client", "vanilla"),
                    ClientType.VANILLA
                );
#pragma warning restore CS8602
                settings.Set(
                    options,
                    "gameversion",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherVersion?.MVersion,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherVersion>.IsNull(s?.LauncherVersion))
                            return;
#pragma warning disable CS8602
                        s.LauncherVersion.MVersion = v;
#pragma warning restore CS8602
                    }
                );
                settings.Set(
                    options,
                    "fabricversion",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherVersion?.FabricLoaderVersion,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherVersion>.IsNull(s?.LauncherVersion))
                            return;
#pragma warning disable CS8602
                        s.LauncherVersion.FabricLoaderVersion = v;
#pragma warning restore CS8602
                    }
                );
                settings.Set(
                    options,
                    "quiltversion",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherVersion?.QuiltLoaderVersion,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherVersion>.IsNull(s?.LauncherVersion))
                            return;
#pragma warning disable CS8602
                        s.LauncherVersion.QuiltLoaderVersion = v;
#pragma warning restore CS8602
                    }
                );
                settings.Set(
                    options,
                    "username",
                    ArgumentConverter.ToNonNullString,
                    s => s?.LauncherUsername?.Username,
                    (s, v) =>
                    {
                        if (ObjectValidator<LauncherUsername>.IsNull(s?.LauncherUsername))
                            return;
#pragma warning disable CS8602
                        s.LauncherUsername.Username = v;
#pragma warning restore CS8602
                    }
                );
                MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
            }
        );

        return Task.CompletedTask;
    }
}
