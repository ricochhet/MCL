using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Enums.Services;
using MCL.Core.Extensions;
using MCL.Core.Helpers.Java;
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
        ConfigService.Save();
        Config config = ConfigService.Load();
        if (config == null)
            return;

        LocalizationService.Init(config.LauncherPath, Language.ENGLISH);
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

        SevenZipService.Init(config.SevenZipConfig);
        ModdingService.Init(config.LauncherPath, config.ModConfig);

        if (args.Length <= 0)
            return;

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-java",
            async () =>
            {
                JavaDownloadService.Init(
                    config.LauncherPath,
                    config.MinecraftUrls,
                    JavaVersionHelper.GetDownloadedMCVersionJava(
                        config.LauncherPath,
                        config.LauncherVersion,
                        config.LauncherSettings
                    ),
                    config.LauncherSettings.JavaRuntimePlatform
                );

                await JavaDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-minecraft",
            async () =>
            {
                MinecraftDownloadService.Init(
                    config.LauncherPath,
                    config.LauncherVersion,
                    config.LauncherSettings,
                    config.MinecraftUrls
                );
                await MinecraftDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-loader",
            async () =>
            {
                FabricLoaderDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.FabricUrls);
                await FabricLoaderDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-fabric-installer",
            async () =>
            {
                FabricInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.FabricUrls);
                if (!await FabricInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    FabricInstallerLaunchArgsHelper.Default(
                        config.LauncherPath,
                        config.LauncherVersion,
                        FabricInstallerType.CLIENT
                    ),
                    config.LauncherSettings.JavaRuntimeType
                );
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-loader",
            async () =>
            {
                QuiltLoaderDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.QuiltUrls);
                await QuiltLoaderDownloadService.Download();
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-quilt-installer",
            async () =>
            {
                QuiltInstallerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.QuiltUrls);
                if (!await QuiltInstallerDownloadService.Download())
                    return;

                JavaLaunchHelper.Launch(
                    config,
                    QuiltInstallerLaunchArgsHelper.Default(
                        config.LauncherPath,
                        config.LauncherVersion,
                        FabricInstallerType.CLIENT
                    ),
                    config.LauncherSettings.JavaRuntimeType
                );
            }
        );

        await CommandLine.ProcessArgumentAsync(
            args,
            "--dl-paper-server",
            async () =>
            {
                PaperServerDownloadService.Init(config.LauncherPath, config.LauncherVersion, config.PaperUrls);
                await PaperServerDownloadService.Download();
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--launch",
            () =>
            {
                MinecraftLaunchHelper.Launch(
                    config.LauncherPath,
                    config.LauncherVersion,
                    config.LauncherSettings,
                    config.LauncherUsername,
                    config
                );
            }
        );

        CommandLine.ProcessArgument(
            args,
            "--mods",
            () =>
            {
                ModdingService.Save("fabric-mods");
                ModdingService.Deploy(
                    ModdingService.Load("fabric-mods"),
                    VFS.FromCwd(config.LauncherPath.Path, "mods")
                );
                config.Save(ModdingService.ModConfig);
            }
        );
    }
}
