using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Enums.Services;
using MCL.Core.Extensions;
using MCL.Core.Helpers.Java;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Helpers.Minecraft;
using MCL.Core.Helpers.MinecraftFabric;
using MCL.Core.Helpers.MinecraftQuilt;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Models.Web;
using MCL.Core.Services.Java;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Minecraft;
using MCL.Core.Services.MinecraftFabric;
using MCL.Core.Services.MinecraftQuilt;
using MCL.Core.Services.Modding;
using MCL.Core.Services.Paper;
using MCL.Core.Services.SevenZip;
using MCL.Core.Services.Web;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.Launcher";
        Log.Add(new NativeLogger());
        Log.Add(new FileStreamLogger(ConfigService.LogFilePath));
        MCLauncherUsername launcherUsername = new(username: "Player1337");
        MCLauncherPath launcherPath =
            new(
                path: LaunchPathHelper.Path,
                modPath: LaunchPathHelper.ModPath,
                fabricInstallerPath: LaunchPathHelper.FabricInstallerPath,
                quiltInstallerPath: LaunchPathHelper.QuiltInstallerPath,
                paperInstallerPath: LaunchPathHelper.PaperInstallerPath,
                languageLocalizationPath: LaunchPathHelper.LanguageLocalizationPath
            );
        MCLauncherVersion launcherVersion =
            new(
                version: "1.20.4",
                versionType: "release",
                fabricInstallerVersion: "1.0.0",
                fabricLoaderVersion: "0.15.9",
                quiltInstallerVersion: "0.9.1",
                quiltLoaderVersion: "0.24.0"
            );
        MCLauncherSettings launcherSettings =
            new(
                LauncherType.RELEASE,
                ClientType.FABRIC,
                Platform.WINDOWS,
                FabricInstallerType.CLIENT,
                JavaRuntimeType.JAVA_RUNTIME_GAMMA,
                JavaRuntimePlatform.WINDOWSX64
            );

        LocalizationService.Init(launcherPath, Language.ENGLISH);
        NotificationService.Init(
            (Notification notification) =>
            {
                Log.Base(notification.LogLevel, notification.Message);
            }
        );
        NotificationService.Add(new(NativeLogLevel.Info, "log.initialized"));
        RequestDataService.Init(
            (RequestData requestData) =>
            {
                NotificationService.Add(new(NativeLogLevel.Info, "request.get", [requestData.URL]));
            }
        );

        Request.JsonSerializerOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        Request.HttpClientTimeOut = TimeSpan.FromMinutes(1);
        Watermark.Draw(ConfigService.WatermarkText);

        ConfigService.Save();
        Config config = ConfigService.Load();
        if (config == null)
            return;

        SevenZipService.Init(config.SevenZipConfig);
        ModdingService.Init(launcherPath, config.ModConfig);

        if (args.Length <= 0)
            return;

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadService.Init(
                    launcherPath,
                    config.MinecraftUrls,
                    JavaVersionHelper.GetDownloadedMCVersionJava(launcherPath, launcherVersion, launcherSettings),
                    launcherSettings.JavaRuntimePlatform
                );

                await JavaDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MCDownloadService.Init(launcherPath, launcherVersion, launcherSettings, config.MinecraftUrls);
                await MCDownloadService.Download();
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

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                MCFabricInstallerDownloadService.Init(launcherPath, launcherVersion, config.FabricUrls);
                if (!await MCFabricInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    FabricInstallerLaunchArgsHelper.Default(launcherPath, launcherVersion, FabricInstallerType.CLIENT),
                    launcherSettings.JavaRuntimeType
                );
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-loader",
            async () =>
            {
                MCQuiltLoaderDownloadService.Init(launcherPath, launcherVersion, config.QuiltUrls);
                await MCQuiltLoaderDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-installer",
            async () =>
            {
                MCQuiltInstallerDownloadService.Init(launcherPath, launcherVersion, config.QuiltUrls);
                if (!await MCQuiltInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    QuiltInstallerLaunchArgsHelper.Default(launcherPath, launcherVersion, FabricInstallerType.CLIENT),
                    launcherSettings.JavaRuntimeType
                );
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-paper-server",
            async () =>
            {
                PaperServerDownloadService.Init(launcherPath, launcherVersion, config.PaperUrls);
                await PaperServerDownloadService.Download();
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MinecraftLaunchHelper.Launch(launcherPath, launcherVersion, launcherSettings, launcherUsername, config);
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--mods",
            () =>
            {
                ModdingService.Save("fabric-mods");
                ModdingService.Deploy(ModdingService.Load("fabric-mods"), VFS.FromCwd(launcherPath.Path, "mods"));
                config.Save(ModdingService.ModConfig);
            }
        );
    }
}
