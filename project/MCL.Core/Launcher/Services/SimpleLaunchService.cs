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
using System.Linq;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Resolvers;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Launcher.Services;

public static class SimpleLaunchService
{
    /// <summary>
    /// Launch the game process specified by a launch file.
    /// </summary>
    public static void Init(string filePath, Settings? settings)
    {
        if (!VFS.Exists(filePath))
            return;

        if (
            ObjectValidator<LauncherSettings>.IsNull(settings?.LauncherSettings)
            || ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion)
            || ObjectValidator<LauncherUsername>.IsNull(settings?.LauncherUsername)
        )
            return;

        string[] lines = VFS.ReadAllLines(filePath);
        Dictionary<string, string> options = [];
        foreach (string line in lines)
            options = options.Concat(CommandLine.ParseKeyValuePairs(line)).ToDictionary();
        options = options.GroupBy(a => a).Select(a => a.Last()).ToDictionary();

        settings!.LauncherSettings!.ClientType = EnumResolver.Parse(
            options.GetValueOrDefault("client", "vanilla"),
            ClientType.VANILLA
        );
        settings!.LauncherVersion!.MVersion = options.GetValueOrDefault(
            "gameversion",
            settings!.LauncherVersion!.MVersion
        );
        settings!.LauncherVersion!.FabricLoaderVersion = options.GetValueOrDefault(
            "fabricversion",
            settings!.LauncherVersion!.FabricLoaderVersion
        );
        settings!.LauncherVersion!.QuiltLoaderVersion = options.GetValueOrDefault(
            "quiltversion",
            settings!.LauncherVersion!.QuiltLoaderVersion
        );
        settings!.LauncherUsername!.Username = options.GetValueOrDefault(
            "username",
            settings!.LauncherUsername!.Username
        );

        NotificationService.Info("launcher.simple.launch");
        MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
    }
}
