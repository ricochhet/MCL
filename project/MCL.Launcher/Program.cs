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
using MCL.Core.Launcher.Services;
using MCL.Core.Managers;
using MCL.Core.MiniCommon.IO;
using MCL.Core.MiniCommon.Logger;
using MCL.Core.MiniCommon.Logger.Enums;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        VFS.FileSystem.Cwd = VFS.GetRelativePath(Environment.CurrentDirectory);

        Console.Title = "MCL.Launcher";
        Log.Add(new NativeLogger(NativeLogLevel.Info));
        Log.Add(new FileStreamLogger(SettingsProvider.LogFilePath));
        await ServiceManager.Init();

        if (args.Length <= 0)
        {
            MLaunchProvider.Launch(SettingsProvider.SimpleMLaunchFilePath, ServiceManager.Settings);
            return;
        }

        await CommandManager.Init(args);
    }
}
