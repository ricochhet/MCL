using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Modding.Models;

namespace MCL.Core.Launcher.Extensions;

public static class SettingsExt
{
    public static Settings Save(this Settings settings, ClientType clientType, JvmArguments jvmArguments)
    {
        switch (clientType)
        {
            case ClientType.VANILLA:
                settings.MinecraftArgs = jvmArguments;
                break;
            case ClientType.FABRIC:
                settings.FabricArgs = jvmArguments;
                break;
            case ClientType.QUILT:
                settings.QuiltArgs = jvmArguments;
                break;
        }

        SettingsService.Save(settings);
        return settings;
    }

    public static Settings Save(this Settings settings, ModSettings modSettings)
    {
        settings.ModSettings = modSettings;
        SettingsService.Save(settings);
        return settings;
    }
}
