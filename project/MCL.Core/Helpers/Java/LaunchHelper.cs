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

        ProcessHelper.RunProcess(
            VFS.Combine(
                JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType),
                config.JavaConfig.JavaExecutable
            ),
            jvmArguments.Build(),
            workingDirectory,
            false,
            new()
            {
                {
                    config.JavaConfig.JavaHomeEnvironmentVariable,
                    JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType)
                }
            }
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

        switch (clientType)
        {
            case ClientType.VANILLA:
                if (!JvmArgumentsExist(config, config.MinecraftArgs))
                    return;
                ProcessHelper.RunProcess(
                    VFS.Combine(
                        JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType),
                        config.JavaConfig.JavaExecutable
                    ),
                    config.MinecraftArgs.Build(),
                    workingDirectory,
                    false,
                    new()
                    {
                        {
                            config.JavaConfig.JavaHomeEnvironmentVariable,
                            JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType)
                        }
                    }
                );
                break;
            case ClientType.FABRIC:
                if (!JvmArgumentsExist(config, config.FabricArgs))
                    return;
                ProcessHelper.RunProcess(
                    VFS.Combine(
                        JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType),
                        config.JavaConfig.JavaExecutable
                    ),
                    config.FabricArgs.Build(),
                    workingDirectory,
                    false,
                    new()
                    {
                        {
                            config.JavaConfig.JavaHomeEnvironmentVariable,
                            JavaPathResolver.JavaRuntimeBin(workingDirectory, javaRuntimeType)
                        }
                    }
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
