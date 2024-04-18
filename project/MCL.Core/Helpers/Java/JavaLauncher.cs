using System;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Helpers.Java;

public static class JavaLauncher
{
    public static void Launch(Settings settings, JvmArguments jvmArguments, JavaRuntimeType javaRuntimeType)
    {
        string workingDirectory = Environment.CurrentDirectory;
        if (settings == null || string.IsNullOrWhiteSpace(workingDirectory))
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), settings.JavaSettings.JavaExecutable);
        RunJavaProcess(settings, workingDirectory, jvmArguments, javaExe, javaHome);
    }

    public static void Launch(
        Settings settings,
        string workingDirectory,
        ClientType clientType,
        JavaRuntimeType javaRuntimeType
    )
    {
        if (settings == null || string.IsNullOrWhiteSpace(workingDirectory))
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(settings, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), settings.JavaSettings.JavaExecutable);

        switch (clientType)
        {
            case ClientType.VANILLA:
                if (!JvmArgumentsExist(settings, settings.MinecraftArgs))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.MinecraftArgs, javaExe, javaHome);
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(settings, settings.FabricArgs))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.FabricArgs, javaExe, javaHome);
                break;
            case ClientType.QUILT:
                if (!JvmArgumentsExist(settings, settings.QuiltArgs))
                    return;
                RunJavaProcess(settings, workingDirectory, settings.QuiltArgs, javaExe, javaHome);
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
            new() { { settings.JavaSettings.JavaHomeEnvironmentVariable, javaHome } }
        );
    }

    private static bool JvmArgumentsExist(Settings settings, JvmArguments jvmArguments)
    {
        if (settings == null)
            return false;

        if (jvmArguments == null)
            return false;

        if (jvmArguments.Arguments == null)
            return false;

        if (jvmArguments.Arguments.Count <= 0)
            return false;

        return true;
    }
}
