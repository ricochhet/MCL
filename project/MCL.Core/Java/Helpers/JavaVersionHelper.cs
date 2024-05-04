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

using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Helpers;
using MCL.Core.Minecraft.Models;
using MiniCommon.Resolvers;
using MiniCommon.Validation;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Java.Helpers;

public static class JavaVersionHelper
{
    /// <summary>
    /// Find the Java runtime version specified for the specified MVersion.
    /// </summary>
    public static JavaRuntimeType? GetMVersionJava(
        LauncherPath? launcherPath,
        LauncherVersion? launcherVersion,
        LauncherSettings? launcherSettings
    )
    {
        if (Validate.For.IsNullOrWhiteSpace([launcherVersion?.MVersion]))
            return launcherSettings?.JavaRuntimeType;
        MVersionDetails? versionDetails = VersionHelper.GetVersionDetails(launcherPath, launcherVersion);
        if (Validate.For.IsNullOrWhiteSpace([versionDetails?.JavaVersion?.Component]))
            return launcherSettings?.JavaRuntimeType;
        return EnumResolver.Parse(
            versionDetails!.JavaVersion!.Component,
            launcherSettings?.JavaRuntimeType ?? JavaRuntimeType.JAVA_RUNTIME_GAMMA
        );
    }
}
