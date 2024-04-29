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
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon.CommandParser;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.Servers.Paper.Helpers;
using MCL.Core.Servers.Paper.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadPaperServer : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings? settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            new()
            {
                Name = "dl-paper-server",
                Parameters =
                [
                    new() { Name = "gameversion", Optional = true },
                    new() { Name = "paperversion", Optional = true },
                    new() { Name = "update", Optional = true }
                ]
            },
            async options =>
            {
                if (ObjectValidator<Settings>.IsNull(settings))
                    return;

                _launcherVersion.MVersion = options.GetValueOrDefault("gameversion", "latest");
                _launcherVersion.PaperServerVersion = options.GetValueOrDefault("paperversion", "latest");
                if (!bool.TryParse(options.GetValueOrDefault("update", "false"), out bool update))
                    return;
                if (
                    ObjectValidator<string>.IsNullOrWhiteSpace(
                        [_launcherVersion.MVersion, _launcherVersion.PaperServerVersion]
                    )
                )
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;
                if (!await PaperVersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;

                PaperServerDownloadService.Init(
                    settings!?.LauncherPath,
                    settings!?.LauncherVersion,
                    settings!?.LauncherInstance,
                    settings!?.PaperUrls
                );
                await PaperServerDownloadService.Download(loadLocalVersionManifest: true);
            }
        );
    }
}
