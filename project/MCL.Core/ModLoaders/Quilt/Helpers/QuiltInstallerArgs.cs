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

using MCL.Core.Java.Extensions;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Enums;
using MCL.Core.ModLoaders.Quilt.Resolvers;

namespace MCL.Core.ModLoaders.Quilt.Helpers;

public static class QuiltInstallerArgs
{
    /// <summary>
    /// The default JvmArguments to run the Quilt installer.
    /// </summary>
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        QuiltInstallerType installerType
    )
    {
        if (
            ObjectValidator<string>.IsNullOrWhiteSpace(
                [launcherVersion?.MVersion, launcherVersion?.QuiltInstallerVersion, launcherVersion?.QuiltLoaderVersion]
            )
        )
            return null;

        JvmArguments jvmArguments = new();
        jvmArguments.Add("-jar \"{0}\"", [QuiltPathResolver.InstallerPath(launcherPath, launcherVersion)]);
        jvmArguments.Add(
            $"install {QuiltInstallerTypeResolver.ToString(installerType)} {0} {1}",
            [launcherVersion.MVersion, launcherVersion.QuiltLoaderVersion]
        );
        jvmArguments.Add(installerType, QuiltInstallerType.INSTALL_SERVER, "--download-server");
        jvmArguments.Add("--install-dir=\"{0}\"", [launcherPath.MPath]);
        jvmArguments.Add("--no-profile");

        return jvmArguments;
    }
}
