using MCL.Core.Java.Models;
using MCL.Core.Launcher.Extensions;
using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Extensions;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperServerArgs
{
    public static JvmArguments DefaultJvmArguments(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        if (!launcherVersion.VersionsExists())
            return default;

        JvmArguments jvmArguments = new();
        jvmArguments.Add("-Xms{0}m", ["4096"]);
        jvmArguments.Add("-Xmx{0}m", ["4096"]);
        jvmArguments.Add("-XX:+AlwaysPreTouch");
        jvmArguments.Add("-XX:+DisableExplicitGC");
        jvmArguments.Add("-XX:+ParallelRefProcEnabled");
        jvmArguments.Add("-XX:+PerfDisableSharedMem");
        jvmArguments.Add("-XX:+UnlockExperimentalVMOptions");
        jvmArguments.Add("-XX:+UseG1GC");
        jvmArguments.Add("-XX:G1HeapRegionSize=8M");
        jvmArguments.Add("-XX:G1HeapWastePercent=5");
        jvmArguments.Add("-XX:G1MaxNewSizePercent=40");
        jvmArguments.Add("-XX:G1MixedGCCountTarget=4");
        jvmArguments.Add("-XX:G1MixedGCLiveThresholdPercent=90");
        jvmArguments.Add("-XX:G1NewSizePercent=30");
        jvmArguments.Add("-XX:G1RSetUpdatingPauseTimePercent=5");
        jvmArguments.Add("-XX:G1ReservePercent=20");
        jvmArguments.Add("-XX:InitiatingHeapOccupancyPercent=15");
        jvmArguments.Add("-XX:MaxGCPauseMillis=200");
        jvmArguments.Add("-XX:MaxTenuringThreshold=1");
        jvmArguments.Add("-XX:SurvivorRatio=32");
        jvmArguments.Add("-Dusing.aikars.flags=https://mcflags.emc.gs");
        jvmArguments.Add("-Daikars.new.flags=true");
        jvmArguments.Add("-jar {0} {1}", [PaperPathResolver.JarPath(launcherPath, launcherVersion), "nogui"]);

        return jvmArguments;
    }
}
