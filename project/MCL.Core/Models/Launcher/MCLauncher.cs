using MCL.Core.Enums;
using MCL.Core.Enums.Java;

namespace MCL.Core.Models.Launcher;

public class MCLauncher(
    MCLauncherPath _launcherPath,
    MCLauncherVersion _launcherVersion,
    ClientTypeEnum _clientType,
    JavaRuntimeTypeEnum _javaRuntimeType,
    JavaRuntimePlatformEnum _javaRuntimePlatform
)
{
    public MCLauncherPath MCLauncherPath { get; set; } = _launcherPath;
    public MCLauncherVersion MCLauncherVersion { get; set; } = _launcherVersion;
    public ClientTypeEnum ClientType { get; set; } = _clientType;
    public JavaRuntimeTypeEnum JavaRuntimeType { get; set; } = _javaRuntimeType;
    public JavaRuntimePlatformEnum JavaRuntimePlatform { get; set; } = _javaRuntimePlatform;
}
