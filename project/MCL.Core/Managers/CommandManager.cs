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
using MCL.Core.Commands.Downloaders;
using MCL.Core.Commands.Launcher;
using MCL.Core.Commands.Modding;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.CommandParser.Commands;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Logger;

namespace MCL.Core.Managers;

public static class CommandManager
{
    public static Settings? Settings { get; private set; }

    public static async Task Init(string[] args)
    {
        try
        {
            List<IBaseCommand> commands = [];

            commands.Add(new DownloadJava());
            commands.Add(new DownloadMinecraft());
            commands.Add(new DownloadFabricInstaller());
            commands.Add(new DownloadFabricLoader());
            commands.Add(new DownloadQuiltInstaller());
            commands.Add(new DownloadQuiltLoader());
            commands.Add(new DownloadPaperServer());
            commands.Add(new LaunchMinecraft());
            commands.Add(new LaunchPaperServer());
            commands.Add(new DeployMods());
            commands.Add(new Help());

            foreach (IBaseCommand command in commands)
                await command.Init(args, ServiceManager.Settings);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.ToString());
            return;
        }
    }
}
