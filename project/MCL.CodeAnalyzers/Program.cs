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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.CodeAnalyzers.Commands;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Enums;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Logger;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;

namespace MCL.CodeAnalyzers;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.CodeAnalyzers";
        Log.Add(new NativeLogger(NativeLogLevel.Info));
        Log.Add(new FileStreamLogger(SettingsService.LogFilePath, NativeLogLevel.Info));
        LocalizationService.Init(SettingsService.LocalizationPath, Language.ENGLISH);
        NotificationService.OnNotificationAdded(
            (Notification notification) =>
            {
                Log.Base(notification.LogLevel, notification.Message);
            }
        );
        NotificationService.Info("log.initialized");
        SettingsService.Init();
        Settings settings = SettingsService.Load();
        if (ObjectValidator<Settings>.IsNull(settings))
            return;
        Watermark.Draw(SettingsService.WatermarkText);

        if (args.Length <= 0)
            return;

        List<ILauncherCommand> commands = [];
        commands.Add(new AnalyzeCode());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, settings);
    }
}
