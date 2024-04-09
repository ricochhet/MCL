using System;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Enums.Services;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Logger;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;
using MCL.Core.Providers;
using MCL.Core.Providers.Java;
using MCL.Core.Providers.Minecraft;
using MCL.Core.Providers.MinecraftFabric;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;
using MCL.Core.Services;

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

        MCLauncherUsername launcherUsername = new(username: "Ricochet");
        MCLauncherPath launcherPath =
            new(
                path: "./.minecraft",
                modPath: "./.minecraft-mods",
                fabricInstallerPath: "./.minecraft-fabric",
                languageLocalizationPath: "./.language"
            );
        MCLauncherVersion launcherVersion =
            new(
                version: "1.20.4",
                versionType: "release",
                fabricInstallerVersion: "1.0.0",
                fabricLoaderVersion: "0.15.9"
            );
        MCLauncher launcher =
            new(
                launcherUsername,
                launcherPath,
                launcherVersion,
                ClientTypeEnum.FABRIC,
                JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA,
                JavaRuntimePlatformEnum.WINDOWSX64
            );

        LocalizationService.Init(launcherPath, LanguageEnum.ENGLISH, true);
        ModdingService.Init(launcherPath, config.ModConfig);
        ModdingService.Save("fabric-mods");
        ModdingService.Deploy(ModdingService.Load("fabric-mods"), VFS.Combine(launcherPath.Path, "mods"));

        if (args.Length <= 0)
            return;

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadProvider javaDownloadProvider =
                    new(
                        launcher.MCLauncherPath,
                        config.MinecraftUrls,
                        JavaVersionHelper.GetDownloadedMCVersionJava(
                            launcher.MCLauncherPath,
                            launcher.MCLauncherVersion,
                            launcher.JavaRuntimeType
                        ),
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
                    new(
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion,
                        PlatformEnum.WINDOWS,
                        config.MinecraftUrls
                    );
                if (!await downloadProvider.DownloadAll())
                    return;
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                MCFabricInstallerDownloadProvider downloadProvider =
                    new(launcher.MCLauncherPath, launcher.MCLauncherVersion, config.FabricUrls);
                if (!await downloadProvider.DownloadAll())
                    return;
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async () =>
            {
                MCFabricLoaderDownloadProvider downloadProvider = new(launcherPath, launcherVersion, config.FabricUrls);
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
                jvmArguments.Add(
                    new LaunchArg(
                        "-Djava.library.path={0}",
                        [MinecraftPathResolver.Libraries(launcher.MCLauncherVersion)]
                    )
                );
                jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.brand={0}", ["mcl"]));
                jvmArguments.Add(new LaunchArg("-Dminecraft.launcher.version={0}", ["1.0.0"]));
                jvmArguments.Add(
                    new LaunchArg(
                        "-cp {0} {1}",
                        [
                            ClassPathHelper.CreateClassPath(launcher.MCLauncherPath, launcher.MCLauncherVersion),
                            ClientTypeEnumResolver.ToString(launcher.ClientType)
                        ]
                    )
                );
                jvmArguments.Add(new LaunchArg("--username {0}", [launcherUsername.ValidateUsername()]));
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
                jvmArguments.Add(new LaunchArg("--uuid {0}", [launcherUsername.UUID()]));
                jvmArguments.Add(new LaunchArg("--clientId {0}", ["0"]));
                jvmArguments.Add(new LaunchArg("--xuid {0}", ["0"]));
                jvmArguments.Add(new LaunchArg("--version {0}", [launcherVersion.Version]));
                jvmArguments.Add(new LaunchArg("--versionType {0}", [launcherVersion.VersionType]));

                ConfigProvider.Write(ConfigHelper.Write(launcher.ClientType, config, jvmArguments));
                JavaLaunchHelper.Launch(config, "./.minecraft/", launcher.ClientType, launcher.JavaRuntimeType);
            }
        );
    }
}
