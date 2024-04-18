using MCL.Core.Enums;
using MCL.Core.Extensions.Java;
using MCL.Core.Extensions.Minecraft;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Launcher;

public static class LaunchArgs
{
    public static JvmArguments DefaultJvmArguments(
        LauncherPath launcherPath,
        LauncherVersion launcherVersion,
        LauncherSettings launcherSettings,
        LauncherUsername launcherUsername
    )
    {
        if (!launcherVersion.VersionsExists())
            return default;

        JvmArguments jvmArguments = new();
        string libraries = MinecraftPathResolver.Libraries(launcherVersion);

        jvmArguments.Add("-Xms{0}m", ["4096"]);
        jvmArguments.Add("-Xmx{0}m", ["4096"]);
        jvmArguments.Add("-XX:+UnlockExperimentalVMOptions");
        jvmArguments.Add("-XX:+UseG1GC");
        jvmArguments.Add("-XX:G1NewSizePercent=20");
        jvmArguments.Add("-XX:G1ReservePercent=20");
        jvmArguments.Add("-XX:MaxGCPauseMillis=50");
        jvmArguments.Add("-XX:G1HeapRegionSize=32M");
        jvmArguments.Add("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
        jvmArguments.Add("-Djava.library.path={0}", [libraries]);
        jvmArguments.Add("-Dminecraft.client.jar={0}", [MinecraftPathResolver.ClientLibrary(launcherVersion)]);
        jvmArguments.Add("-Djna.tmpdir={0}", [libraries]);
        jvmArguments.Add("-Dorg.lwjgl.system.SharedLibraryExtractPath={0}", [libraries]);
        jvmArguments.Add("-Dio.netty.native.workdir={0}", [libraries]);
        jvmArguments.Add(
            launcherSettings.LauncherType,
            LauncherType.DEBUG,
            "-Dlog4j2.configurationFile={0}",
            [MinecraftPathResolver.LoggingPath(launcherVersion)]
        );
        jvmArguments.Add("-Dminecraft.launcher.brand={0}", ["MCL"]);
        jvmArguments.Add("-Dminecraft.launcher.version={0}", ["1.0.0"]);
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.VANILLA,
            "-DMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA)]
        );
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.FABRIC,
            "-DFabricMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA)]
        );
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.QUILT,
            "-DFabricMcEmu={0}",
            [ClientTypeResolver.ToString(ClientType.VANILLA)]
        );
        jvmArguments.Add("-Dlog4j2.formatMsgNoLookups=true");
        jvmArguments.Add("-Djava.rmi.server.useCodebaseOnly=true");
        jvmArguments.Add("-Dcom.sun.jndi.rmi.object.trustURLCodebase=false");
        jvmArguments.Add(
            "-cp {0} {1}",
            [
                ClassPathHelper.CreateClassPath(launcherPath, launcherVersion, launcherSettings.JavaRuntimePlatform),
                ClientTypeResolver.ToString(launcherSettings.ClientType)
            ]
        );
        jvmArguments.Add("--username {0}", [launcherUsername.ValidateUsername()]);
        jvmArguments.Add("--userType {0}", ["legacy"]);
        jvmArguments.Add("--gameDir {0}", ["."]);
        jvmArguments.Add("--assetIndex {0}", [AssetHelper.GetAssetId(launcherPath, launcherVersion)]);
        jvmArguments.Add("--assetsDir {0}", ["assets"]);
        jvmArguments.Add("--accessToken {0}", ["1337535510N"]);
        jvmArguments.Add("--uuid {0}", [launcherUsername.UUID()]);
        jvmArguments.Add("--clientId {0}", ["0"]);
        jvmArguments.Add("--xuid {0}", ["0"]);
        jvmArguments.Add("--version {0}", [launcherVersion.Version]);
        jvmArguments.Add("--versionType {0}", [launcherVersion.VersionType]);

        return jvmArguments;
    }
}
