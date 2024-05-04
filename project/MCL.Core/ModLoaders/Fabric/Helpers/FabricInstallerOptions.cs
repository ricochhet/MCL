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
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Resolvers;
using MCL.Core.ModLoaders.Interfaces.Helpers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public class FabricInstallerOptions : IModLoaderInstallerOptions<FabricInstallerType>
{
    /// <inheritdoc />
    public static MOption[]? DefaultJvmArguments(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        FabricInstallerType installerType
    )
    {
        if (
            Validate.For.IsNullOrWhiteSpace(
                [
                    launcherVersion?.MVersion,
                    launcherVersion?.FabricInstallerVersion,
                    launcherVersion?.FabricLoaderVersion,
                    launcherPath?.MPath
                ]
            )
        )
        {
            return null;
        }

        return
        [
            new MOption
            {
                Arg = "-jar \"{0}\" {1}",
                ArgParams = [FabricPathResolver.InstallerPath(launcherPath, launcherVersion), "client"]
            },
            new MOption
            {
                Arg = "-dir \"{0}\" {1}",
                ArgParams = [launcherPath!.MPath, FabricInstallerTypeResolver.ToString(installerType)]
            },
            new MOption { Arg = "-mcversion {0}", ArgParams = [launcherVersion!.MVersion] },
            new MOption { Arg = "-loader {0}", ArgParams = [launcherVersion!.FabricLoaderVersion] },
            new MOption { Arg = "-noprofile" }
        ];
    }
}
