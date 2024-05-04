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
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MiniCommon.IO;
using MiniCommon.IO.Helpers;
using MiniCommon.Logger.Enums;
using MiniCommon.Validation.Operators;
using MiniCommon.Validation.Validators;

namespace MCL.Core.Java.Helpers;

public static class JavaLauncher
{
    /// <summary>
    /// Starts a Java runtime process with the specified arguments.
    /// </summary>
    public static void Launch(
        Settings? settings,
        string workingDirectory,
        JvmArguments? jvmArguments,
        JavaRuntimeType? javaRuntimeType,
        string javaHome
    )
    {
        if (
            ClassValidator.IsNull(settings)
            || StringValidator.IsNullOrWhiteSpace([workingDirectory])
            || ClassValidator.IsNull(jvmArguments)
        )
        {
            return;
        }
        if (!VFS.Exists(workingDirectory))
            return;
        string _javaHome = javaHome;
        if (StringValidator.IsNullOrWhiteSpace([javaHome], NativeLogLevel.Debug))
            _javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(
            JavaPathResolver.JavaRuntimeBin(_javaHome),
            settings!.JavaSettings?.Executable ?? StringOperator.Empty()
        );
        RunJavaProcess(settings, workingDirectory, jvmArguments, javaExe, _javaHome);
    }

    /// <summary>
    /// Starts a Java runtime process with the arguments specified by ClientType.
    /// </summary>
    public static void Launch(
        Settings settings,
        string workingDirectory,
        ClientType? clientType,
        JavaRuntimeType? javaRuntimeType,
        string javaHome
    )
    {
        if (StringValidator.IsNullOrWhiteSpace([workingDirectory]))
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string _javaHome = javaHome;
        if (StringValidator.IsNullOrWhiteSpace([javaHome], NativeLogLevel.Debug))
            _javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(
            JavaPathResolver.JavaRuntimeBin(_javaHome),
            settings!.JavaSettings?.Executable ?? StringOperator.Empty()
        );

        switch (clientType)
        {
            case ClientType.VANILLA
            or ClientType.CUSTOM:
                if (!JvmArgumentsExist(settings, settings.MJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.MJvmArguments, javaExe, _javaHome);
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(settings, settings.FabricJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.FabricJvmArguments, javaExe, _javaHome);
                break;
            case ClientType.QUILT:
                if (!JvmArgumentsExist(settings, settings.QuiltJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.QuiltJvmArguments, javaExe, _javaHome);
                break;
        }
    }

    /// <summary>
    /// Starts a Java runtime process with the corresponding environment variables.
    /// </summary>
    public static void RunJavaProcess(
        Settings? settings,
        string workingDirectory,
        JvmArguments? jvmArguments,
        string javaExe,
        string javaHome
    )
    {
        ProcessHelper.RunProcess(
            javaExe,
            jvmArguments?.Build() ?? StringOperator.Empty(),
            workingDirectory,
            false,
            new() { { settings?.JavaSettings?.HomeEnvironmentVariable ?? StringOperator.Empty(), javaHome } }
        );
    }

    /// <summary>
    /// Validates the JvmArguments are not null, or empty.
    /// </summary>
    private static bool JvmArgumentsExist(Settings settings, JvmArguments? jvmArguments) =>
        ClassValidator.IsNotNull(settings) && ListValidator.IsNotNullOrEmpty(jvmArguments?.Arguments);
}
