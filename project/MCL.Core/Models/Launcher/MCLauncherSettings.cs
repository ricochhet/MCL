using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Enums.MinecraftFabric;

namespace MCL.Core.Models.Launcher;

public class MCLauncherSettings(
    LauncherType launcherType,
    ClientType clientType,
    FabricInstallerType fabricInstallerType,
    JavaRuntimeType javaRuntimeType,
    JavaRuntimePlatform javaRuntimePlatform
)
{
    public LauncherType LauncherType { get; set; } = launcherType;
    public ClientType ClientType { get; set; } = clientType;
    public FabricInstallerType FabricInstallerType { get; set; } = fabricInstallerType;
    public JavaRuntimeType JavaRuntimeType { get; set; } = javaRuntimeType;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = javaRuntimePlatform;
}
