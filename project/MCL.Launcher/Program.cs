using System;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Enums.Services;
using MCL.Core.Extensions;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Providers.MinecraftFabric;
using MCL.Core.Services;
using MCL.Core.Services.Java;
using MCL.Core.Services.Minecraft;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.Launcher";
        LogBase.Add(new NativeLogger());
        LogBase.Add(new FileStreamLogger());
        MCLauncherUsername launcherUsername = new(username: "Player1337");
        MCLauncherPath launcherPath =
            new(
                path: "./.minecraft",
                modPath: "./.minecraft-mods",
                fabricInstallerPath: "./.minecraft-fabric",
                languageLocalizationPath: "./.localization"
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
                LauncherType.RELEASE,
                ClientType.FABRIC,
                JavaRuntimeType.JAVA_RUNTIME_GAMMA,
                JavaRuntimePlatform.WINDOWSX64
            );

        LocalizationService.Init(launcherPath, Language.ENGLISH);
        NotificationService.Init(
            (Notification notification) =>
            {
                LogBase.Base(notification.LogLevel, notification.Message);
            }
        );

        NotificationService.Add(new Notification(NativeLogLevel.Info, "log.initialized"));
        Request.SetJsonSerializerOptions(new() { WriteIndented = true });
        Request.SetHttpClientTimeOut(TimeSpan.FromMinutes(1));
        Watermark.Draw(ConfigService.WatermarkText);

        ConfigService.Save();
        Config config = ConfigService.Load();
        if (config == null)
        {
            NotificationService.Add(
                new Notification(
                    NativeLogLevel.Error,
                    "launcher.config.missing",
                    [ConfigService.ConfigFileName, ConfigService.DataPath]
                )
            );
            CommandLine.Pause();
            return;
        }

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
                JavaDownloadService.Init(
                    launcher.MCLauncherPath,
                    config.MinecraftUrls,
                    JavaVersionHelper.GetDownloadedMCVersionJava(
                        launcher.MCLauncherPath,
                        launcher.MCLauncherVersion,
                        launcher.JavaRuntimeType
                    ),
                    JavaRuntimePlatform.WINDOWSX64
                );

                await JavaDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MCDownloadService.Init(
                    launcher.MCLauncherPath,
                    launcher.MCLauncherVersion,
                    Platform.WINDOWS,
                    config.MinecraftUrls
                );
                await MCDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                MCFabricInstallerDownloadService.Init(
                    launcher.MCLauncherPath,
                    launcher.MCLauncherVersion,
                    config.FabricUrls
                );
                await MCFabricInstallerDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async () =>
            {
                MCFabricLoaderDownloadService.Init(launcherPath, launcherVersion, config.FabricUrls);
                await MCFabricLoaderDownloadService.Download();
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                config.Save(launcher.ClientType, LaunchArgsHelper.Default(launcher));
                config.Save(ModdingService.ModConfig);
                ConfigService.Save(config);
                JavaLaunchHelper.Launch(
                    config,
                    launcher.MCLauncherPath.Path,
                    launcher.ClientType,
                    launcher.JavaRuntimeType
                );
            }
        );
    }
}
