using MCL.Core.Enums;
using MCL.Core.Models;
using MCL.Core.Models.Java;
using MCL.Core.Services.Modding;

namespace MCL.Core.Extensions;

public static class ConfigExt
{
    public static Config Save(this Config config, ClientType clientType, JvmArguments jvmArguments)
    {
        switch (clientType)
        {
            case ClientType.VANILLA:
                config.MinecraftArgs = jvmArguments;
                break;
            case ClientType.FABRIC:
                config.FabricArgs = jvmArguments;
                break;
        }

        return config;
    }

    public static Config Save(this Config config, ModConfig modConfig)
    {
        config.ModConfig = modConfig;
        return config;
    }
}
