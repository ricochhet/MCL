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
using CodeAnalyzers.Commands;
using MiniCommon.BuildInfo;
using MiniCommon.CommandParser.Commands;
using MiniCommon.Enums;
using MiniCommon.Interfaces;
using MiniCommon.IO;
using MiniCommon.Logger;
using MiniCommon.Logger.Enums;
using MiniCommon.Models;
using MiniCommon.Providers;

namespace CodeAnalyzers;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        VFS.FileSystem.Cwd = AppDomain.CurrentDomain.BaseDirectory;

        Console.Title = "CodeAnalyzers";
        Log.Add(new NativeLogger(NativeLogLevel.Info));
        Log.Add(new FileStreamLogger(AssemblyConstants.LogFilePath, NativeLogLevel.Info));
        LocalizationProvider.Init(AssemblyConstants.LocalizationPath, Language.ENGLISH);
        NotificationProvider.OnNotificationAdded(
            (Notification notification) => Log.Base(notification.LogLevel, notification.Message)
        );
        NotificationProvider.Info("log.initialized");
        Watermark.Draw(AssemblyConstants.WatermarkText);

        if (args.Length == 0)
            return;

        List<IBaseCommand<object>> commands = [];
        commands.Add(new AnalyzeCode<object>());
        commands.Add(new Help<object>());

        foreach (IBaseCommand<object> command in commands)
            await command.Init(args, null);
    }
}
