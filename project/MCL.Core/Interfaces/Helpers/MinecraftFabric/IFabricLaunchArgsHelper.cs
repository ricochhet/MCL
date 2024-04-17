using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Helpers.MinecraftFabric;

public interface IFabricLaunchArgsHelper<in T>
{
    public static abstract JvmArguments Default(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        T installerType
    );
}
