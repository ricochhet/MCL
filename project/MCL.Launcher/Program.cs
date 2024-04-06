using System;
using System.Threading.Tasks;
using MCL.Core.Config;
using MCL.Core.Config.Minecraft;
using MCL.Core.Enums;
using MCL.Core.Helpers;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
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
        ConfigModel config = ConfigProvider.Read();
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

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MCConfigArgs minecraftArgs =
                    new()
                    {
                        InitialHeapSize = "4096",
                        MaxHeapSize = "4096",
                        ClassPath = ClassPathHelper.CreateClassPath("./.minecraft/", "1.20.4"),
                        MainClass = ClientTypeEnumResolver.ToString(ClientTypeEnum.VANILLA),
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
                    };

                LaunchHelper.Launch(MinecraftArgGenerator.Generate(minecraftArgs), "./.minecraft/");
            }
        );
    }
}
