using MCL.Core.Enums;
using MCL.Core.Enums.Java;

namespace MCL.Core.Models.Launcher;

public class MCLauncher(
    MCLauncherUsername _launcherUsername,
    MCLauncherPath _launcherPath,
    MCLauncherVersion _launcherVersion,
    LauncherType _launcherType,
    ClientType _clientType,
    JavaRuntimeType _javaRuntimeType,
    JavaRuntimePlatform _javaRuntimePlatform
)
{
    public MCLauncherUsername MCLauncherUsername { get; set; } = _launcherUsername;
    public MCLauncherPath MCLauncherPath { get; set; } = _launcherPath;
    public MCLauncherVersion MCLauncherVersion { get; set; } = _launcherVersion;
    public LauncherType LauncherType { get; set; } = _launcherType;
    public ClientType ClientType { get; set; } = _clientType;
    public JavaRuntimeType JavaRuntimeType { get; set; } = _javaRuntimeType;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = _javaRuntimePlatform;
}
