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
using System.Threading.Tasks;
using MCL.Core.Launcher.Providers;
using MCL.Core.Managers;
using MiniCommon.BuildInfo;
using MiniCommon.IO;
using MiniCommon.Logger;
using MiniCommon.Logger.Enums;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        VFS.FileSystem.Cwd = AppDomain.CurrentDomain.BaseDirectory;

        Console.Title = "MCL.Launcher";
        Log.Add(new NativeLogger(NativeLogLevel.Info));
        Log.Add(new FileStreamLogger(AssemblyConstants.LogFilePath));
        await ServiceManager.Init();

        if (args.Length == 0)
        {
            MLaunchProvider.Launch(SettingsProvider.SimpleMLaunchFilePath, ServiceManager.Settings);
            return;
        }

        await CommandManager.Init(args);
    }
}
