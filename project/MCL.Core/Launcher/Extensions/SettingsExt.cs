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
                if (settings.MJvmArguments != jvmArguments)
                {
                    settings.MJvmArguments = jvmArguments;
                    SettingsService.Save(settings);
                }
                break;
            case ClientType.FABRIC:
                if (settings.FabricJvmArguments != jvmArguments)
                {
                    settings.FabricJvmArguments = jvmArguments;
                    SettingsService.Save(settings);
                }
                break;
            case ClientType.QUILT:
                if (settings.QuiltJvmArguments != jvmArguments)
                {
                    settings.QuiltJvmArguments = jvmArguments;
                    SettingsService.Save(settings);
                }
                break;
        }

        return settings;
    }

    public static Settings Save(this Settings settings, ModSettings modSettings)
    {
        if (settings.ModSettings != modSettings)
        {
            settings.ModSettings = modSettings;
            SettingsService.Save(settings);
        }
        return settings;
    }

    public static Settings Save(this Settings settings, LauncherInstance launcherInstance)
    {
        if (settings.LauncherInstance != launcherInstance)
        {
            settings.LauncherInstance = launcherInstance;
            SettingsService.Save(settings);
        }
        return settings;
    }
}
