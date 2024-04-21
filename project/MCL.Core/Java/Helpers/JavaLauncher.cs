using System;
using System.Collections.Generic;
using MCL.Core.Java.Enums;
using MCL.Core.Java.Models;
using MCL.Core.Java.Resolvers;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Helpers;

namespace MCL.Core.Java.Helpers;

public static class JavaLauncher
{
    public static void Launch(Settings settings, JvmArguments jvmArguments, JavaRuntimeType javaRuntimeType)
    {
        string workingDirectory = Environment.CurrentDirectory;
        if (
            ObjectValidator<Settings>.IsNull(settings) || ObjectValidator<string>.IsNullOrWhiteSpace([workingDirectory])
        )
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), settings.JavaSettings.Executable);
        RunJavaProcess(settings, workingDirectory, jvmArguments, javaExe, javaHome);
    }

    public static void Launch(
        Settings settings,
        string workingDirectory,
        JvmArguments jvmArguments,
        JavaRuntimeType javaRuntimeType
    )
    {
        if (
            ObjectValidator<Settings>.IsNull(settings) || ObjectValidator<string>.IsNullOrWhiteSpace([workingDirectory])
        )
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), settings.JavaSettings.Executable);
        RunJavaProcess(settings, workingDirectory, jvmArguments, javaExe, javaHome);
    }

    public static void Launch(
        Settings settings,
        string workingDirectory,
        ClientType clientType,
        JavaRuntimeType javaRuntimeType
    )
    {
        if (
            ObjectValidator<Settings>.IsNull(settings) || ObjectValidator<string>.IsNullOrWhiteSpace([workingDirectory])
        )
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), settings.JavaSettings.Executable);

        switch (clientType)
        {
            case ClientType.VANILLA:
                if (!JvmArgumentsExist(settings, settings.MJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.MJvmArguments, javaExe, javaHome);
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(settings, settings.FabricJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.FabricJvmArguments, javaExe, javaHome);
                break;
            case ClientType.QUILT:
                if (!JvmArgumentsExist(settings, settings.QuiltJvmArguments))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.QuiltJvmArguments, javaExe, javaHome);
                break;
        }
    }

    private static void RunJavaProcess(
        Settings settings,
        string workingDirectory,
        JvmArguments jvmArguments,
        string javaExe,
        string javaHome
    )
    {
        ProcessHelper.RunProcess(
            javaExe,
            jvmArguments.Build(),
            workingDirectory,
            false,
            new() { { settings.JavaSettings.HomeEnvironmentVariable, javaHome } }
        );
    }

    private static bool JvmArgumentsExist(Settings settings, JvmArguments jvmArguments) =>
        ObjectValidator<Settings>.IsNotNull(settings)
        && ObjectValidator<LaunchArg>.IsNotNullOrEmpty(jvmArguments?.Arguments);
}
