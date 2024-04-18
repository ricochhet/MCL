using MCL.Core.Java.Enums;
using MCL.Core.Launcher.Enums;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.Launcher.Models;

public class LauncherSettings
{
    public LauncherType LauncherType { get; set; } = LauncherType.RELEASE;
    public ClientType ClientType { get; set; } = ClientType.FABRIC;
    public FabricInstallerType FabricInstallerType { get; set; } = FabricInstallerType.CLIENT;
    public QuiltInstallerType QuiltInstallerType { get; set; } = QuiltInstallerType.CLIENT;
    public JavaRuntimeType JavaRuntimeType { get; set; } = JavaRuntimeType.JAVA_RUNTIME_GAMMA;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = JavaRuntimePlatform.WINDOWSX64;

    public LauncherSettings() { }

    public LauncherSettings(
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
