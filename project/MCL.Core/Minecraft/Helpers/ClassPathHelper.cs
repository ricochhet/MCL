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
using System.IO;
using System.Linq;
using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MiniCommon.IO;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Minecraft.Helpers;

public static class ClassPathHelper
{
    /// <summary>
    /// Get a list of class libraries for the specified MVersion and Loader versions.
    /// </summary>
    public static string GetClassLibraries(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherInstance? launcherInstance,
        LauncherSettings? launcherSettings
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.MVersion]) || Validate.For.IsNull(launcherInstance))
        {
            return string.Empty;
        }

        string separator = launcherSettings?.JavaRuntimePlatform switch
        {
            JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386
            or JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64
                => ":",
            JavaRuntimePlatform.WINDOWSX64 or JavaRuntimePlatform.WINDOWSX86 or JavaRuntimePlatform.WINDOWSARM64 => ";",
            _ => throw new NotImplementedException("Unsupported OS."),
        };

        LauncherLoader? mLoader = launcherInstance!.Versions.Find(a => a.Version == launcherVersion!.MVersion);
        if (Validate.For.IsNull(mLoader))
            return string.Empty;
        string[] libraries = mLoader!.Libraries.Prepend(MPathResolver.ClientLibrary(launcherVersion)).ToArray();

        switch (launcherSettings.ClientType)
        {
            case ClientType.VANILLA:
                break;
            case ClientType.FABRIC:
                if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.FabricLoaderVersion]))
                    return string.Empty;
                LauncherLoader? fabricLoader = launcherInstance!.FabricLoaders.Find(a =>
                    a.Version == launcherVersion!.FabricLoaderVersion
                );
                libraries = [.. libraries, .. fabricLoader?.Libraries];
                break;
            case ClientType.QUILT:
                if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.QuiltLoaderVersion]))
                    return string.Empty;
                LauncherLoader? quiltLoader = launcherInstance!.QuiltLoaders.Find(a =>
                    a.Version == launcherVersion!.QuiltLoaderVersion
                );
                libraries = [.. libraries, .. quiltLoader?.Libraries];
                break;
            case ClientType.CUSTOM:
                libraries = VFS.GetFiles(MPathResolver.LibraryPath(launcherPath), "*")
                    .Prepend(MPathResolver.ClientLibrary(launcherVersion))
                    .ToArray();
                break;
        }

        return string.Join(
            separator,
            libraries.Select(a =>
                a.Replace(launcherPath!.MPath, string.Empty)
                    .Replace(
                        Path.DirectorySeparatorChar + MPathResolver.BaseLibrariesPath,
                        MPathResolver.BaseLibrariesPath
                    )
            )
        );
    }
}
