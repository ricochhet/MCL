using System;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Resolvers;

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
                    new("./.minecraft", "1.20.4", PlatformEnumResolver.Parse("windows"));
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
                MinecraftArgHelper minecraftArgHelper =
                    new(
                        _initialHeapSize: "4096",
                        _maxHeapSize: "4096",
                        _classPath: ClassPathHelper.CreateClassPath("./.minecraft/", "1.20.4"),
                        _mainClass: MinecraftArgs.MainClass,
                        _username: "Ricochet",
                        _userType: "legacy",
                        _gameDir: ".",
                        _assetIndex: MinecraftArgs.AssetIndexId("./.minecraft/").ToString(),
                        _assetsDir: "assets",
                        _uuid: CryptographyHelper.UUID("Ricochet"),
                        _clientId: "0",
                        _xuid: "0",
                        _accessToken: "1337535510N",
                        _version: "1.20.4",
                        _versionType: "release",
                        [
                            "-XX:+UnlockExperimentalVMOptions",
                            "-XX:+UseG1GC",
                            "-XX:G1NewSizePercent=20",
                            "-XX:G1ReservePercent=20",
                            "-XX:MaxGCPauseMillis=50",
                            "-XX:G1HeapRegionSize=32M",
                            "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump",
                            $"-Djava.library.path={MinecraftArgs.Libraries("1.20.4")}",
                            $"-Dminecraft.launcher.brand=mcl",
                            $"-Dminecraft.launcher.version=1.0.0",
                        ]
                    );

                MinecraftLaunchHelper minecraftLaunchHelper = new(minecraftArgHelper);
                minecraftLaunchHelper.Launch("./.minecraft/");
            }
        );
    }
}
