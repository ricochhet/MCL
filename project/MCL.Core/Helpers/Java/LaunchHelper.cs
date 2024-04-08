using System.IO;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Models;
using MCL.Core.Models.Java;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Helpers.Java;

public static class JavaLaunchHelper
{
    public static void Launch(JvmArguments jvmArguments, string workingDirectory)
    {
        ProcessHelper.RunProcess("java", jvmArguments.Build(), workingDirectory, false);
    }

    public static void Launch(JvmArguments jvmArguments, string workingDirectory, JavaRuntimeTypeEnum javaRuntimeType)
    {
        string javaBin = Path.Combine(
            workingDirectory,
            "runtime",
            JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType),
            "bin"
        );
        ProcessHelper.RunProcess(
            Path.Combine(javaBin, "java.exe"),
            jvmArguments.Build(),
            workingDirectory,
            false,
            new() { { "JAVA_HOME", javaBin } }
        );
    }

    public static void Launch(
        Config config,
        string workingDirectory,
        ClientTypeEnum clientType,
        JavaRuntimeTypeEnum javaRuntimeType
    )
    {
        if (config == null)
            return;

        string javaBin = Path.Combine(
            workingDirectory,
            "runtime",
            JavaRuntimeTypeEnumResolver.ToString(javaRuntimeType),
            "bin"
        );

        switch (clientType)
        {
            case ClientTypeEnum.VANILLA:
                if (!JvmArgumentsExist(config, config.MinecraftArgs))
                    return;
                ProcessHelper.RunProcess(
                    Path.Combine(javaBin, "java.exe"),
                    config.MinecraftArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { "JAVA_HOME", javaBin } }
                );
                break;
            case ClientTypeEnum.FABRIC:
                if (!JvmArgumentsExist(config, config.FabricArgs))
                    return;
                ProcessHelper.RunProcess(
                    Path.Combine(javaBin, "java.exe"),
                    config.FabricArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { "JAVA_HOME", javaBin } }
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
