using MCL.Core.Enums;
using MCL.Core.Models;
using MCL.Core.Models.Java;

namespace MCL.Core.Helpers.Minecraft;

public static class ConfigHelper
{
    public static Config Write(ClientTypeEnum clientType, Config config, JvmArguments jvmArguments)
    {
        switch (clientType)
        {
            case ClientTypeEnum.VANILLA:
                config.MinecraftArgs = jvmArguments;
                break;
            case ClientTypeEnum.FABRIC:
                config.FabricArgs = jvmArguments;
                break;
        }

        return config;
    }
}
