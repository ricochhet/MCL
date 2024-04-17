using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Modding;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Extensions.Launcher;

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
            case ClientType.QUILT:
                config.QuiltArgs = jvmArguments;
                break;
        }

        ConfigService.Save(config);
        return config;
    }

    public static Config Save(this Config config, ModConfig modConfig)
    {
        config.ModConfig = modConfig;
        ConfigService.Save(config);
        return config;
    }
}