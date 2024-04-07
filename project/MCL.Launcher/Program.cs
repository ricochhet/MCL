using System;
using System.Threading.Tasks;
using MCL.Core.Config;
using MCL.Core.Config.Minecraft;
using MCL.Core.Enums;
using MCL.Core.Helpers;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Web.Minecraft;

namespace MCL.Launcher;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.Launcher";
        LogBase.Add(new NativeLogger());
        LogBase.Add(new FileStreamLogger());
        LogBase.Info("Initialized logger");
        Watermark.Draw(ConfigProvider.WatermarkText);

        ConfigProvider.Write();
        Config config = ConfigProvider.Read();
        if (config == null)
        {
            LogBase.Error(
                $"{ConfigProvider.ConfigFileName} could not be read, make sure it is located in \"{ConfigProvider.DataPath}\" and try again."
            );
            CommandLine.Pause();
            return;
        }

        if (args.Length <= 0)
            return;

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadProvider javaDownloadProvider =
                    new(
                        "./.minecraft",
                        config.MinecraftUrls,
                        JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA,
                        JavaRuntimePlatformEnum.WINDOWSX64
                    );

                if (!await javaDownloadProvider.DownloadAll())
                    return;
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MCDownloadProvider downloadProvider =
                    new("./.minecraft", "1.20.4", PlatformEnum.WINDOWS, config.MinecraftUrls);
                if (!await downloadProvider.DownloadAll())
                    return;
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric",
            async () =>
            {
                MCFabricDownloadProvider downloadProvider = new("./.minecraft-fabric", "1.0.0", config.FabricUrls);
                if (!await downloadProvider.DownloadAll())
                    return;
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                JvmArguments jvmArguments = new();
                jvmArguments.Add(new LaunchArg("-Xms{0}m", ["4096"]));
                jvmArguments.Add(new LaunchArg("-Xmx{0}m", ["4096"]));
                jvmArguments.Add(
                    new LaunchArg(
                        "-cp {0} {1}",
                        [
                            ClassPathHelper.CreateClassPath("./.minecraft/", "1.20.4"),
                            ClientTypeEnumResolver.ToString(ClientTypeEnum.VANILLA)
                        ]
                    )
                );
                jvmArguments.Add(new LaunchArg("--username {0}", ["Ricochet"]));
                jvmArguments.Add(new LaunchArg("--userType {0}", ["legacy"]));
                jvmArguments.Add(new LaunchArg("--gameDir {0}", ["."]));
                jvmArguments.Add(
                    new LaunchArg("--assetIndex {0}", [MinecraftArgsResolver.AssetIndexId("./.minecraft/").ToString()])
                );
                jvmArguments.Add(new LaunchArg("--assetsDir {0}", ["assets"]));
                jvmArguments.Add(new LaunchArg("--accessToken {0}", ["1337535510N"]));
                jvmArguments.Add(new LaunchArg("--uuid {0}", [CryptographyHelper.UUID("Ricochet")]));
                jvmArguments.Add(new LaunchArg("--clientId {0}", ["0"]));
                jvmArguments.Add(new LaunchArg("--xuid {0}", ["0"]));
                jvmArguments.Add(new LaunchArg("--version {0}", ["1.20.4"]));
                jvmArguments.Add(new LaunchArg("--versionType {0}", ["release"]));

                jvmArguments.Add(new LaunchArg("-XX:+UnlockExperimentalVMOptions"));
                jvmArguments.Add(new LaunchArg("-XX:+UseG1GC"));
                jvmArguments.Add(new LaunchArg("-XX:G1NewSizePercent=20"));
                jvmArguments.Add(new LaunchArg("-XX:G1ReservePercent=20"));
                jvmArguments.Add(new LaunchArg("-XX:MaxGCPauseMillis=50"));
                jvmArguments.Add(new LaunchArg("-XX:G1HeapRegionSize=32M"));
                jvmArguments.Add(
                    new LaunchArg(
                        "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump"
                    )
                );
                jvmArguments.Add(new LaunchArg("-Djava.library.path={0}", [MinecraftArgsResolver.Libraries("1.20.4")]));
                jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.brand={0}", ["mcl"]));
                jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.version={0}", ["1.0.0"]));

                JavaLaunchHelper.Launch(jvmArguments.Build(), "./.minecraft/");
            }
        );
    }
}
