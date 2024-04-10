using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Java;
using MCL.Core.Resolvers.Java;

namespace MCL.Core.Helpers.Java;

public static class JavaLaunchHelper
{
    public static void Launch(
        Config config,
        string workingDirectory,
        ClientTypeEnum clientType,
        JavaRuntimeTypeEnum javaRuntimeType
    )
    {
        if (config == null)
            return;

        string javaBin = VFS.Combine(
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
                    VFS.Combine(javaBin, config.JavaConfig.JavaExecutable),
                    config.MinecraftArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaBin } }
                );
                break;
            case ClientTypeEnum.FABRIC:
                if (!JvmArgumentsExist(config, config.FabricArgs))
                    return;
                ProcessHelper.RunProcess(
                    VFS.Combine(javaBin, config.JavaConfig.JavaExecutable),
                    config.FabricArgs.Build(),
                    workingDirectory,
                    false,
                    new() { { config.JavaConfig.JavaHomeEnvironmentVariable, javaBin } }
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
