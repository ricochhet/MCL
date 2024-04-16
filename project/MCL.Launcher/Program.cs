using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MCL.Core.Enums.Services;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Models.Web;
using MCL.Core.Services.Launcher;
using MCL.Core.Services.Modding;
using MCL.Core.Services.SevenZip;
using MCL.Core.Services.Web;
using MCL.Launcher.Commands;

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

        List<ILauncherCommand> commands = [];

        commands.Add(new DownloadJava());
        commands.Add(new DownloadMinecraft());
        commands.Add(new DownloadFabricInstaller());
        commands.Add(new DownloadFabricLoader());
        commands.Add(new DownloadQuiltInstaller());
        commands.Add(new DownloadQuiltLoader());
        commands.Add(new DownloadPaperServer());
        commands.Add(new LaunchMinecraft());
        commands.Add(new DeployMods());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, config);
    }
}
