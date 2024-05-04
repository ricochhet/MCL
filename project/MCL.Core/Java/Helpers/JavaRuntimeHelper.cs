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
using MCL.Core.Java.Enums;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Models;
using MiniCommon.IO;
using MiniCommon.Providers;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Java.Helpers;

public static class JavaRuntimeHelper
{
    /// <summary>
    /// Attempts to find a pre-installed Java runtime environment in the specified working directory.
    /// If no environment is found, attempt to look in the PATH variables for 'JAVA_HOME'.
    /// </summary>
    public static string FindJavaRuntimeEnvironment(
        Settings? settings,
        string workingDirectory,
        JavaRuntimeType? javaRuntimeType
    )
    {
        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);

        if (!VFS.Exists(javaHome))
        {
            if (StringValidator.IsNullOrWhiteSpace([settings?.JavaSettings?.HomeEnvironmentVariable]))
                return javaHome;

            string? javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                settings!.JavaSettings!.HomeEnvironmentVariable
            );
            if (StringValidator.IsNullOrWhiteSpace([javaHomeEnvironmentVariable]))
                return javaHome;

            NotificationProvider.Info("error.missing.java");
            return javaHomeEnvironmentVariable!;
        }

        return javaHome;
    }
}
