using MCL.Core.Enums;
using MCL.Core.Enums.Java;

namespace MCL.Core.Models.Launcher;

#pragma warning disable IDE0079
#pragma warning disable S107
public class MCLauncher(
    MCLauncherUsername launcherUsername,
    MCLauncherPath launcherPath,
    MCLauncherVersion launcherVersion,
    LauncherType launcherType,
    ClientType clientType,
    Platform platform,
    JavaRuntimeType javaRuntimeType,
    JavaRuntimePlatform javaRuntimePlatform
)
{
    public MCLauncherUsername MCLauncherUsername { get; set; } = launcherUsername;
    public MCLauncherPath MCLauncherPath { get; set; } = launcherPath;
    public MCLauncherVersion MCLauncherVersion { get; set; } = launcherVersion;
    public LauncherType LauncherType { get; set; } = launcherType;
    public ClientType ClientType { get; set; } = clientType;
    public Platform Platform { get; set; } = platform;
    public JavaRuntimeType JavaRuntimeType { get; set; } = javaRuntimeType;
    public JavaRuntimePlatform JavaRuntimePlatform { get; set; } = javaRuntimePlatform;
}
#pragma warning restore
