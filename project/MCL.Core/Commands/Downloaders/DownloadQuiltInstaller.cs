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
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Providers;
using MCL.Core.ModLoaders.Quilt.Wrappers;

namespace MCL.Core.Commands.Downloaders;

public class DownloadQuiltInstaller : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings? settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            new()
            {
                Name = "dl-quilt-installer",
                Parameters =
                [
                    new() { Name = "installerversion", Optional = true },
                    new() { Name = "loaderversion", Optional = true },
                    new() { Name = "update", Optional = true },
                    new() { Name = "javapath", Optional = true }
                ],
                Description = LocalizationProvider.Translate("command.download-quilt-installer")
            },
            async options =>
            {
                _launcherVersion.QuiltInstallerVersion = options.GetValueOrDefault("installerversion", "latest");
                _launcherVersion.QuiltLoaderVersion = options.GetValueOrDefault("loaderversion", "latest");
                if (!bool.TryParse(options.GetValueOrDefault("update", "false"), out bool update))
                    return;
                await QuiltInstallerDownloadWrapper.DownloadAndRun(
                    settings,
                    _launcherVersion,
                    options.GetValueOrDefault("javapath", string.Empty),
                    update
                );
            }
        );
    }
}
