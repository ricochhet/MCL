using MCL.Core.Interfaces.Helpers.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Paper;

namespace MCL.Core.Helpers.MinecraftFabric;

public class PaperLaunchArgsHelper : IPaperLaunchArgsHelper
{
    public static JvmArguments Default(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        JvmArguments jvmArguments = new();
        jvmArguments.Add(new LaunchArg("-Xms{0}m", ["4096"]));
        jvmArguments.Add(new LaunchArg("-Xmx{0}m", ["4096"]));
        jvmArguments.Add(new LaunchArg("-XX:+AlwaysPreTouch"));
        jvmArguments.Add(new LaunchArg("-XX:+DisableExplicitGC"));
        jvmArguments.Add(new LaunchArg("-XX:+ParallelRefProcEnabled"));
        jvmArguments.Add(new LaunchArg("-XX:+PerfDisableSharedMem"));
        jvmArguments.Add(new LaunchArg("-XX:+UnlockExperimentalVMOptions"));
        jvmArguments.Add(new LaunchArg("-XX:+UseG1GC"));
        jvmArguments.Add(new LaunchArg("-XX:G1HeapRegionSize=8M"));
        jvmArguments.Add(new LaunchArg("-XX:G1HeapWastePercent=5"));
        jvmArguments.Add(new LaunchArg("-XX:G1MaxNewSizePercent=40"));
        jvmArguments.Add(new LaunchArg("-XX:G1MixedGCCountTarget=4"));
        jvmArguments.Add(new LaunchArg("-XX:G1MixedGCLiveThresholdPercent=90"));
        jvmArguments.Add(new LaunchArg("-XX:G1NewSizePercent=30"));
        jvmArguments.Add(new LaunchArg("-XX:G1RSetUpdatingPauseTimePercent=5"));
        jvmArguments.Add(new LaunchArg("-XX:G1ReservePercent=20"));
        jvmArguments.Add(new LaunchArg("-XX:InitiatingHeapOccupancyPercent=15"));
        jvmArguments.Add(new LaunchArg("-XX:MaxGCPauseMillis=200"));
        jvmArguments.Add(new LaunchArg("-XX:MaxTenuringThreshold=1"));
        jvmArguments.Add(new LaunchArg("-XX:SurvivorRatio=32"));
        jvmArguments.Add(new LaunchArg("-Dusing.aikars.flags=https://mcflags.emc.gs"));
        jvmArguments.Add(new LaunchArg("-Daikars.new.flags=true"));
        jvmArguments.Add(
            new LaunchArg("-jar {0} {1}", [PaperPathResolver.DownloadedJarPath(launcherPath, launcherVersion), "nogui"])
        );

        return jvmArguments;
    }
}
