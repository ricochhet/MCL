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
using System.Linq;
using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ClassPathHelper
{
    public static string GetClassLibraries(
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance,
        LauncherSettings launcherSettings
    )
    {
        if (ObjectValidator<string>.IsNullOrWhiteSpace([launcherVersion?.MVersion]))
            return string.Empty;

        string separator = launcherSettings.JavaRuntimePlatform switch
        {
            JavaRuntimePlatform.LINUX
            or JavaRuntimePlatform.LINUXI386
            or JavaRuntimePlatform.MACOS
            or JavaRuntimePlatform.MACOSARM64
                => ":",
            JavaRuntimePlatform.WINDOWSX64 or JavaRuntimePlatform.WINDOWSX86 or JavaRuntimePlatform.WINDOWSARM64 => ";",
            _ => throw new NotImplementedException("Unsupported OS."),
        };

        LauncherLoader mLoader = launcherInstance.Versions.Find(a => a.Version == launcherVersion.MVersion);
        if (ObjectValidator<LauncherLoader>.IsNull(mLoader))
            return string.Empty;

        string[] libraries = mLoader.Libraries.Prepend(MPathResolver.ClientLibrary(launcherVersion)).ToArray();

        switch (launcherSettings.ClientType)
        {
            case ClientType.VANILLA:
                libraries = ManageVanillaLibraries(libraries, launcherInstance);
                break;
            case ClientType.FABRIC:
                libraries = ManageFabricLibraries(libraries, launcherVersion, launcherInstance);
                break;
            case ClientType.QUILT:
                libraries = ManageQuiltLibraries(libraries, launcherVersion, launcherInstance);
                break;
        }

        return string.Join(separator, libraries);
    }

    private static string[] ManageVanillaLibraries(string[] libraries, LauncherInstance launcherInstance)
    {
        string[] managedLibraries = libraries;

        foreach (LauncherLoader loader in launcherInstance.FabricLoaders.Concat(launcherInstance.QuiltLoaders))
        {
            libraries = libraries.Except(loader.Libraries).ToArray();
        }

        return managedLibraries;
    }

    private static string[] ManageFabricLibraries(
        string[] libraries,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance
    )
    {
        string[] managedLibraries = libraries;

        // Remove all quilt specific libraries.
        foreach (LauncherLoader loader in launcherInstance.QuiltLoaders)
        {
            managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
        }

        // Remove all fabric specific libraries that don't belong to the current version.
        foreach (LauncherLoader loader in launcherInstance.FabricLoaders)
        {
            if (loader.Version != launcherVersion.FabricLoaderVersion)
                managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
            else
                managedLibraries = [.. managedLibraries, .. loader.Libraries];
        }

        return managedLibraries;
    }

    private static string[] ManageQuiltLibraries(
        string[] libraries,
        LauncherVersion launcherVersion,
        LauncherInstance launcherInstance
    )
    {
        string[] managedLibraries = libraries;

        // Remove all fabric specific libraries.
        foreach (LauncherLoader loader in launcherInstance.FabricLoaders)
        {
            managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
        }

        // Remove all quilt specific libraries that don't belong to the current version.
        foreach (LauncherLoader loader in launcherInstance.QuiltLoaders)
        {
            if (loader.Version != launcherVersion.QuiltLoaderVersion)
                managedLibraries = managedLibraries.Except(loader.Libraries).ToArray();
            else
                managedLibraries = [.. managedLibraries, .. loader.Libraries];
        }

        return managedLibraries;
    }
}
