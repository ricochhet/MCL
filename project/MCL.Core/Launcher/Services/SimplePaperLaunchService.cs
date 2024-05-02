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
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon.CommandParser.Helpers;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Services;
using MCL.Core.MiniCommon.Validation;

namespace MCL.Core.Launcher.Services;

public static class SimplePaperLaunchService
{
    /// <summary>
    /// Launch the game process specified by a launch file.
    /// </summary>
    public static void Init(string filePath, Settings? settings)
    {
        if (!VFS.Exists(filePath))
            return;

        if (ObjectValidator<LauncherVersion>.IsNull(settings?.LauncherVersion))
            return;

        Dictionary<string, string> options = CommandFileHelper.Commands(filePath);

        settings!.LauncherVersion!.MVersion = options.GetValueOrDefault(
            "gameversion",
            settings!.LauncherVersion!.MVersion
        );
        settings!.LauncherVersion!.PaperServerVersion = options.GetValueOrDefault(
            "paperversion",
            settings!.LauncherVersion!.PaperServerVersion
        );

        NotificationService.Info("launcher.simple.launch");
        MinecraftLauncher.Launch(settings, options.GetValueOrDefault("javapath", string.Empty));
    }
}