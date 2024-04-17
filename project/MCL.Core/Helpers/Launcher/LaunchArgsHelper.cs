using MCL.Core.Enums;
using MCL.Core.Extensions.Java;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Interfaces.Helpers.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Launcher;

public class LaunchArgsHelper : ILaunchArgsHelper
{
    public static JvmArguments Default(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion,
        MCLauncherSettings launcherSettings,
        MCLauncherUsername launcherUsername
    )
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return default;

        JvmArguments jvmArguments = new();
        string libraries = MinecraftPathResolver.Libraries(launcherVersion);

        jvmArguments.Add(new LaunchArg("-Xms{0}m", ["4096"]));
        jvmArguments.Add(new LaunchArg("-Xmx{0}m", ["4096"]));
        jvmArguments.Add(new LaunchArg("-XX:+UnlockExperimentalVMOptions"));
        jvmArguments.Add(new LaunchArg("-XX:+UseG1GC"));
        jvmArguments.Add(new LaunchArg("-XX:G1NewSizePercent=20"));
        jvmArguments.Add(new LaunchArg("-XX:G1ReservePercent=20"));
        jvmArguments.Add(new LaunchArg("-XX:MaxGCPauseMillis=50"));
        jvmArguments.Add(new LaunchArg("-XX:G1HeapRegionSize=32M"));
        jvmArguments.Add(
            new LaunchArg("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump")
        );
        jvmArguments.Add(new LaunchArg("-Djava.library.path={0}", [libraries]));
        jvmArguments.Add(
            new LaunchArg("-Dminecraft.client.jar={0}", [MinecraftPathResolver.ClientLibrary(launcherVersion)])
        );
        jvmArguments.Add(new LaunchArg("-Djna.tmpdir={0}", [libraries]));
        jvmArguments.Add(new LaunchArg("-Dorg.lwjgl.system.SharedLibraryExtractPath={0}", [libraries]));
        jvmArguments.Add(new LaunchArg("-Dio.netty.native.workdir={0}", [libraries]));
        jvmArguments.Add(
            launcherSettings.LauncherType,
            LauncherType.DEBUG,
            new LaunchArg("-Dlog4j2.configurationFile={0}", [MinecraftPathResolver.LoggingPath(launcherVersion)])
        );
        jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.brand={0}", ["MCL"]));
        jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.version={0}", ["1.0.0"]));
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.VANILLA,
            new LaunchArg("-DMcEmu={0}", [ClientTypeResolver.ToString(ClientType.VANILLA)])
        );
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.FABRIC,
            new LaunchArg("-DFabricMcEmu={0}", [ClientTypeResolver.ToString(ClientType.VANILLA)])
        );
        jvmArguments.Add(
            launcherSettings.ClientType,
            ClientType.QUILT,
            new LaunchArg("-DFabricMcEmu={0}", [ClientTypeResolver.ToString(ClientType.VANILLA)])
        );
        jvmArguments.Add(new LaunchArg("-Dlog4j2.formatMsgNoLookups=true"));
        jvmArguments.Add(new LaunchArg("-Djava.rmi.server.useCodebaseOnly=true"));
        jvmArguments.Add(new LaunchArg("-Dcom.sun.jndi.rmi.object.trustURLCodebase=false"));
        jvmArguments.Add(
            new LaunchArg(
                "-cp {0} {1}",
                [
                    ClassPathHelper.CreateClassPath(
                        launcherPath,
                        launcherVersion,
                        launcherSettings.JavaRuntimePlatform
                    ),
                    ClientTypeResolver.ToString(launcherSettings.ClientType)
                ]
            )
        );
        jvmArguments.Add(new LaunchArg("--username {0}", [launcherUsername.ValidateUsername()]));
        jvmArguments.Add(new LaunchArg("--userType {0}", ["legacy"]));
        jvmArguments.Add(new LaunchArg("--gameDir {0}", ["."]));
        jvmArguments.Add(new LaunchArg("--assetIndex {0}", [AssetHelper.GetAssetId(launcherPath, launcherVersion)]));
        jvmArguments.Add(new LaunchArg("--assetsDir {0}", ["assets"]));
        jvmArguments.Add(new LaunchArg("--accessToken {0}", ["1337535510N"]));
        jvmArguments.Add(new LaunchArg("--uuid {0}", [launcherUsername.UUID()]));
        jvmArguments.Add(new LaunchArg("--clientId {0}", ["0"]));
        jvmArguments.Add(new LaunchArg("--xuid {0}", ["0"]));
        jvmArguments.Add(new LaunchArg("--version {0}", [launcherVersion.Version]));
        jvmArguments.Add(new LaunchArg("--versionType {0}", [launcherVersion.VersionType]));

        return jvmArguments;
    }
}
