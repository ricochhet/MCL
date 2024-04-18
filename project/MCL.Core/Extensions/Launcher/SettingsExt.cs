using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Modding;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Extensions.Launcher;

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
