using System;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Helpers.Java;

public static class JavaLaunchHelper
{
    public static void Launch(Config config, JvmArguments jvmArguments, JavaRuntimeType javaRuntimeType)
    {
        string workingDirectory = Environment.CurrentDirectory;
        if (config == null || string.IsNullOrWhiteSpace(workingDirectory))
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(config, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), config.JavaConfig.JavaExecutable);
        RunJavaProcess(config, workingDirectory, jvmArguments, javaExe, javaHome);
    }

    public static void Launch(
        Config config,
        string workingDirectory,
        ClientType clientType,
        JavaRuntimeType javaRuntimeType
    )
    {
        if (config == null || string.IsNullOrWhiteSpace(workingDirectory))
            return;
        if (!VFS.Exists(workingDirectory))
            return;
        string javaHome = JavaRuntimeHelper.FindJavaRuntimeEnvironment(config, workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(JavaPathResolver.JavaRuntimeBin(javaHome), config.JavaConfig.JavaExecutable);

        switch (clientType)
        {
            case ClientType.VANILLA:
                if (!JvmArgumentsExist(config, config.MinecraftArgs))
                    return;
                RunJavaProcess(config, workingDirectory, config.MinecraftArgs, javaExe, javaHome);
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(config, config.FabricArgs))
                    return;
                RunJavaProcess(config, workingDirectory, config.FabricArgs, javaExe, javaHome);
                break;
            case ClientType.QUILT:
                if (!JvmArgumentsExist(config, config.QuiltArgs))
                    return;
                RunJavaProcess(config, workingDirectory, config.QuiltArgs, javaExe, javaHome);
                break;
        }
    }

    private static void RunJavaProcess(
        Config config,
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
            new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaHome } }
        );
    }

    private static bool JvmArgumentsExist(Config config, JvmArguments jvmArguments)
    {
        if (config == null)
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
