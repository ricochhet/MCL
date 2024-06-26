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
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Fabric.Helpers;
using MCL.Core.ModLoaders.Fabric.Services;
using MCL.Core.ModLoaders.Interfaces.Wrappers;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.ModLoaders.Fabric.Wrappers;

public class FabricInstallerDownloadWrapper : IModLoaderInstallerDownloadWrapper
{
    /// <inheritdoc />
    public static async Task<bool> DownloadAndRun(
        Settings? settings,
        LauncherVersion launcherVersion,
        string javaPath,
        bool update
    )
    {
        if (Validate.For.IsNull(settings))
            return false;
        if (Validate.For.IsNullOrWhiteSpace([launcherVersion.FabricInstallerVersion]))
            return false;
        if (!await VersionHelper.SetVersion(settings, launcherVersion, update))
            return false;
        if (!await FabricVersionHelper.SetInstallerVersion(settings, launcherVersion, update))
            return false;
        if (!await FabricVersionHelper.SetLoaderVersion(settings, launcherVersion, update))
            return false;

        FabricInstallerDownloadService downloader =
            new(settings!?.LauncherPath, settings!?.LauncherVersion, settings!?.FabricUrls);
        if (!await downloader.Download(loadLocalVersionManifest: true))
            return false;

        JavaLauncher.Launch(
            settings,
            VFS.FileSystem.Cwd,
            FabricInstallerOptions
                .DefaultJvmArguments(
                    settings!?.LauncherPath,
                    settings!?.LauncherVersion,
                    FabricInstallerType.INSTALL_CLIENT
                )
                ?.JvmArguments(),
            settings!?.LauncherSettings?.JavaRuntimeType,
            javaPath
        );

        return true;
    }
}
