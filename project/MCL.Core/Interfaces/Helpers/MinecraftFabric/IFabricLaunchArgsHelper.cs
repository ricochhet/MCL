using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Helpers.MinecraftFabric;

public interface IFabricLaunchArgsHelper
{
    public static abstract JvmArguments Default(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        FabricInstallerType installerType
    );
}
