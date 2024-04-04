using System;
using System.Threading.Tasks;
using MCL.Core.Config;
using MCL.Core.Config.Minecraft;
using MCL.Core.Helpers;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Providers;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Launcher;

internal class Program
{
    private static void Main(string[] args)
    {
        LogBase.Add(new NativeLogger());
        LogBase.Info("Initialized logger");

        if (args.Length <= 0)
            return;

        CommandLine.ProcessArgument(
            args,
            "--download",
            async () =>
            {
                DownloadProvider downloadProvider =
                    new("./.minecraft", "1.20.4", PlatformEnumResolver.Parse("windows"), new());
                if (!await downloadProvider.RequestDownloads())
                    return;
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--cp",
            () =>
            {
                LogBase.Info(ClassPathHelper.CreateClassPath("./.minecraft/", "1.20.4"));
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MinecraftArgConfig minecraftArgConfig =
                    new(
                        new()
                        {
                            InitialHeapSize = "4096",
                            MaxHeapSize = "4096",
                            ClassPath = ClassPathHelper.CreateClassPath("./.minecraft/", "1.20.4"),
                            MainClass = MinecraftArgsResolver.MainClass,
                            Username = "Ricochet",
                            UserType = "legacy",
                            GameDir = ".",
                            AssetIndex = MinecraftArgsResolver.AssetIndexId("./.minecraft/").ToString(),
                            AssetsDir = "assets",
                            Uuid = CryptographyHelper.UUID("Ricochet"),
                            ClientId = "0",
                            Xuid = "0",
                            AccessToken = "1337535510N",
                            Version = "1.20.4",
                            VersionType = "release",
                            AdditionalArguments =
                            [
                                "-XX:+UnlockExperimentalVMOptions",
                                "-XX:+UseG1GC",
                                "-XX:G1NewSizePercent=20",
                                "-XX:G1ReservePercent=20",
                                "-XX:MaxGCPauseMillis=50",
                                "-XX:G1HeapRegionSize=32M",
                                "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump",
                                $"-Djava.library.path={MinecraftArgsResolver.Libraries("1.20.4")}",
                                $"-Dminecraft.launcher.brand=mcl",
                                $"-Dminecraft.launcher.version=1.0.0",
                            ]
                        }
                    );

                LaunchHelper minecraftLaunchHelper = new(minecraftArgConfig);
                minecraftLaunchHelper.Launch("./.minecraft/");
            }
        );
    }
}
