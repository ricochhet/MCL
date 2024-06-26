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
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MiniCommon.CommandParser.Helpers;
using MiniCommon.IO;
using MiniCommon.Providers;
using MiniCommon.Resolvers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Launcher.Providers;

public static class MLaunchProvider
{
    /// <summary>
    /// Launch the game process specified by a launch file.
    /// </summary>
    public static void Launch(string filePath, Settings? settings)
    {
        if (!VFS.Exists(filePath))
            return;

        if (
            Validate.For.IsNull(settings?.LauncherSettings)
            || Validate.For.IsNull(settings?.LauncherVersion)
            || Validate.For.IsNull(settings?.LauncherUsername)
        )
        {
            return;
        }

        Dictionary<string, string> options = CommandFileHelper.Commands(filePath);

        settings!.LauncherSettings!.ClientType = EnumResolver.Parse(
            options.GetValueOrDefault("client", "vanilla"),
            ClientType.VANILLA
        );
        settings!.LauncherSettings!.AuthType = EnumResolver.Parse(
            options.GetValueOrDefault("online", "offline"),
            AuthType.OFFLINE
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

        NotificationProvider.Info("launcher.simple.launch");
        MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
    }
}
