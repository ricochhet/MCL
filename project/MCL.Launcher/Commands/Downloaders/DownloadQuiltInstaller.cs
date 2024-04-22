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
using MCL.Core.Java.Helpers;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.ModLoaders.Quilt.Enums;
using MCL.Core.ModLoaders.Quilt.Helpers;
using MCL.Core.ModLoaders.Quilt.Resolvers;
using MCL.Core.ModLoaders.Quilt.Services;

namespace MCL.Launcher.Commands.Downloaders;

public class DownloadQuiltInstaller : ILauncherCommand
{
    private static readonly LauncherVersion _launcherVersion = LauncherVersion.Latest();

    public async Task Init(string[] args, Settings settings)
    {
        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-installer",
            async options =>
            {
                _launcherVersion.QuiltInstallerVersion = options.GetValueOrDefault("installerversion") ?? "latest";
                if (!bool.TryParse(options.GetValueOrDefault("update") ?? "false", out bool update))
                    return;
                if (ObjectValidator<string>.IsNullOrWhiteSpace([_launcherVersion.QuiltInstallerVersion]))
                    return;
                if (!await VersionHelper.SetVersion(settings, _launcherVersion, update))
                    return;
                if (!await QuiltVersionHelper.SetInstallerVersion(settings, _launcherVersion, update))
                    return;

                QuiltInstallerDownloadService.Init(settings.LauncherPath, settings.LauncherVersion, settings.QuiltUrls);
                if (!await QuiltInstallerDownloadService.Download(useLocalVersionManifest: true))
                    return;

                JavaLauncher.Launch(
                    settings,
                    QuiltPathResolver.InstallersPath(settings.LauncherPath),
                    QuiltInstallerArgs.DefaultJvmArguments(
                        settings.LauncherPath,
                        settings.LauncherVersion,
                        QuiltInstallerType.INSTALL_CLIENT
                    ),
                    settings.LauncherSettings.JavaRuntimeType
                );
            }
        );
    }
}
