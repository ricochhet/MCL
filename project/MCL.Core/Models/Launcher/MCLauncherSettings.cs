using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Enums.MinecraftQuilt;

namespace MCL.Core.Models.Launcher;

public class MCLauncherSettings
{
    public LauncherType LauncherType { get; set; } = LauncherType.RELEASE;
    public ClientType ClientType { get; set; } = ClientType.FABRIC;
    public FabricInstallerType FabricInstallerType { get; set; } = FabricInstallerType.CLIENT;
    public QuiltInstallerType QuiltInstallerType { get; set; } = QuiltInstallerType.CLIENT;
    public JavaRuntimeType JavaRuntimeType { get; set; } = JavaRuntimeType.JAVA_RUNTIME_GAMMA;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = JavaRuntimePlatform.WINDOWSX64;

    public MCLauncherSettings() { }

    public MCLauncherSettings(
        LauncherType launcherType,
        ClientType clientType,
        FabricInstallerType fabricInstallerType,
        QuiltInstallerType quiltInstallerType,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimePlatform javaRuntimePlatform
    )
    {
        LauncherType = launcherType;
        ClientType = clientType;
        FabricInstallerType = fabricInstallerType;
        QuiltInstallerType = quiltInstallerType;
        JavaRuntimeType = javaRuntimeType;
        JavaRuntimePlatform = javaRuntimePlatform;
    }
}
