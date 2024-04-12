using MCL.Core.Enums;
using MCL.Core.Extensions;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Launcher;

public static class LaunchArgsHelper
{
    public static JvmArguments Default(MCLauncher launcher)
    {
        JvmArguments jvmArguments = new();
        string libraries = MinecraftPathResolver.Libraries(launcher.MCLauncherVersion);

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
            new LaunchArg(
                "-Dminecraft.client.jar={0}",
                [MinecraftPathResolver.ClientLibrary(launcher.MCLauncherVersion)]
            )
        );
        jvmArguments.Add(new LaunchArg("-Djna.tmpdir={0}", [libraries]));
        jvmArguments.Add(new LaunchArg("-Dorg.lwjgl.system.SharedLibraryExtractPath={0}", [libraries]));
        jvmArguments.Add(new LaunchArg("-Dio.netty.native.workdir={0}", [libraries]));
        jvmArguments.Add(
            launcher.LauncherType,
            LauncherType.DEBUG,
            new LaunchArg(
                "-Dlog4j2.configurationFile={0}",
                [MinecraftPathResolver.LoggingPath(launcher.MCLauncherVersion)]
            )
        );
        jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.brand={0}", ["MCL"]));
        jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.version={0}", ["1.0.0"]));
        jvmArguments.Add(
            launcher.ClientType,
            ClientType.VANILLA,
            new LaunchArg("-DMcEmu={0}", [ClientTypeResolver.ToString(ClientType.VANILLA)])
        );
        jvmArguments.Add(
            launcher.ClientType,
            ClientType.FABRIC,
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
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion,
                        launcher.Platform
                    ),
                    ClientTypeResolver.ToString(launcher.ClientType)
                ]
            )
        );
        jvmArguments.Add(new LaunchArg("--username {0}", [launcher.MCLauncherUsername.ValidateUsername()]));
        jvmArguments.Add(new LaunchArg("--userType {0}", ["legacy"]));
        jvmArguments.Add(new LaunchArg("--gameDir {0}", ["."]));
        jvmArguments.Add(
            new LaunchArg(
                "--assetIndex {0}",
                [AssetHelper.GetAssetId(launcher.MCLauncherPath, launcher.MCLauncherVersion)]
            )
        );
        jvmArguments.Add(new LaunchArg("--assetsDir {0}", ["assets"]));
        jvmArguments.Add(new LaunchArg("--accessToken {0}", ["1337535510N"]));
        jvmArguments.Add(new LaunchArg("--uuid {0}", [launcher.MCLauncherUsername.UUID()]));
        jvmArguments.Add(new LaunchArg("--clientId {0}", ["0"]));
        jvmArguments.Add(new LaunchArg("--xuid {0}", ["0"]));
        jvmArguments.Add(new LaunchArg("--version {0}", [launcher.MCLauncherVersion.Version]));
        jvmArguments.Add(new LaunchArg("--versionType {0}", [launcher.MCLauncherVersion.VersionType]));

        return jvmArguments;
    }
}
