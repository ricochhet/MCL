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

using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Interfaces.Helpers;
using MCL.Core.ModLoaders.Quilt.Enums;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public class QuiltInstallerOptions : IModLoaderInstallerOptions<QuiltInstallerType>
{
    /// <inheritdoc />
    public static MOption[]? DefaultJvmArguments(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        QuiltInstallerType installerType
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [
                    launcherVersion?.MVersion,
                    launcherVersion?.QuiltInstallerVersion,
                    launcherVersion?.QuiltLoaderVersion,
                    launcherPath?.MPath
                ]
            )
        )
            return null;

        return
        [
            new MOption
            {
                Arg = "-jar \"{0}\"",
                ArgParams = [QuiltPathResolver.InstallerPath(launcherPath, launcherVersion)]
            },
            new MOption
            {
                Arg = $"install {QuiltInstallerTypeResolver.ToString(installerType)} {0} {1}",
                ArgParams = [launcherVersion!.MVersion, launcherVersion!.QuiltLoaderVersion]
            },
            new MOption { Arg = "--download-server", Condition = installerType == QuiltInstallerType.INSTALL_SERVER },
            new MOption { Arg = "--install-dir=\"{0}\"", ArgParams = [launcherPath!.MPath] },
            new MOption { Arg = "--no-profile" }
        ];
    }
}
