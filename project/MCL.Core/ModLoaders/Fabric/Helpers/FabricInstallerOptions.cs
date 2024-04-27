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

using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon.Validation;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Resolvers;

namespace MCL.Core.ModLoaders.Fabric.Helpers;

public static class FabricInstallerOptions
{
    /// <summary>
    /// The default JvmArguments to run the Fabric installer.
    /// </summary>
    public static MArgument[]? DefaultJvmArguments(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        FabricInstallerType installerType
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [
                    launcherVersion?.MVersion,
                    launcherVersion?.FabricInstallerVersion,
                    launcherVersion?.FabricLoaderVersion
                ]
            )
        )
            return null;

        return
        [
            new MArgument
            {
                Arg = "-jar \"{0}\" {1}",
                ArgParams = [FabricPathResolver.InstallerPath(launcherPath, launcherVersion), "client"]
            },
            new MArgument
            {
                Arg = "-dir \"{0}\" {1}",
                ArgParams =
                [
                    launcherPath?.MPath ?? ValidationShims.StringEmpty(),
                    FabricInstallerTypeResolver.ToString(installerType)
                ]
            },
            new MArgument
            {
                Arg = "-mcversion {0}",
                ArgParams = [launcherVersion?.MVersion ?? ValidationShims.StringEmpty()]
            },
            new MArgument
            {
                Arg = "-loader {0}",
                ArgParams = [launcherVersion?.FabricLoaderVersion ?? ValidationShims.StringEmpty()]
            },
            new MArgument { Arg = "-noprofile" }
        ];
    }
}
