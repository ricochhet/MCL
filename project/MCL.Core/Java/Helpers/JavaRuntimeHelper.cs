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
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon;

namespace MCL.Core.Java.Helpers;

public static class JavaRuntimeHelper
{
    public static string FindJavaRuntimeEnvironment(
        Settings settings,
        string workingDirectory,
        JavaRuntimeType javaRuntimeType
    )
    {
        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);

        if (!VFS.Exists(javaHome))
        {
            string javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                settings.JavaSettings.HomeEnvironmentVariable
            );
            if (ObjectValidator<string>.IsNullOrWhiteSpace([javaHomeEnvironmentVariable]))
                return javaHome;

            NotificationService.Info("error.missing.java");
            return javaHomeEnvironmentVariable;
        }

        return javaHome;
    }
}
