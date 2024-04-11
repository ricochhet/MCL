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
        ClientType clientType,
        JavaRuntimeType javaRuntimeType
    )
    {
        if (config == null || string.IsNullOrWhiteSpace(workingDirectory))
            return;

        string javaBin = VFS.Combine(
            workingDirectory,
            "runtime",
            JavaRuntimeTypeResolver.ToString(javaRuntimeType),
            "bin"
        );

        switch (clientType)
        {
            case ClientType.VANILLA:
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
            case ClientType.FABRIC:
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
