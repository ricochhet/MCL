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

        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(
            JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType),
            config.JavaConfig.JavaExecutable
        );

        if (!VFS.Exists(javaHome) || !VFS.Exists(javaExe))
        {
            string javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                config.JavaConfig.JavaHomeEnvironmentVariable
            );
            if (string.IsNullOrWhiteSpace(javaHomeEnvironmentVariable))
                return;
            javaHome = javaHomeEnvironmentVariable;
            javaExe = VFS.Combine(
                JavaPathResolver.JavaRuntimeBin(javaHomeEnvironmentVariable),
                config.JavaConfig.JavaExecutable
            );
        }

        ProcessHelper.RunProcess(
            javaExe,
            jvmArguments.Build(),
            workingDirectory,
            false,
            new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaHome } }
        );
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

        string javaHome = JavaPathResolver.JavaRuntimeHome(workingDirectory, javaRuntimeType);
        string javaExe = VFS.Combine(
            JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType),
            config.JavaConfig.JavaExecutable
        );

        if (!VFS.Exists(javaHome) || !VFS.Exists(javaExe))
        {
            string javaHomeEnvironmentVariable = Environment.GetEnvironmentVariable(
                config.JavaConfig.JavaHomeEnvironmentVariable
            );
            if (string.IsNullOrWhiteSpace(javaHomeEnvironmentVariable))
                return;
            javaHome = javaHomeEnvironmentVariable;
            javaExe = VFS.Combine(
                JavaPathResolver.JavaRuntimeBin(javaHomeEnvironmentVariable),
                config.JavaConfig.JavaExecutable
            );
        }

        switch (clientType)
        {
            case ClientType.VANILLA:
                if (!JvmArgumentsExist(config, config.MinecraftArgs))
                    return;
                ProcessHelper.RunProcess(
                    javaExe,
                    config.MinecraftArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaHome } }
                );
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(config, config.FabricArgs))
                    return;
                ProcessHelper.RunProcess(
                    javaExe,
                    config.FabricArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaHome } }
                );
                break;
        }
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
