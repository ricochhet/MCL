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

using System.Threading.Tasks;
using MCL.Core.Java.Helpers;
using MCL.Core.Java.Services;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.Interfaces;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadJava : ILauncherCommand
{
    public async Task Init(string[] args, Settings? settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            new() { Name = "dl-java" },
            async _ =>
            {
                JavaDownloadService.Init(
                    settings?.LauncherPath,
                    settings?.MUrls,
                    JavaVersionHelper.GetMVersionJava(
                        settings?.LauncherPath,
                        settings?.LauncherVersion,
                        settings?.LauncherSettings
                    ),
                    settings?.LauncherSettings?.JavaRuntimePlatform
                );

                await JavaDownloadService.Download();
            }
        );
    }
}
