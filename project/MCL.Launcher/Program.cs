using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MCL.Core.FileExtractors.Services;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.MiniCommon.Models;
using MCL.Core.MiniCommon.Services;
using MCL.Core.Modding.Services;
using MCL.Launcher.Commands.Downloaders;
using MCL.Launcher.Commands.Launcher;
using MCL.Launcher.Commands.Modding;

namespace MCL.Launcher;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.Launcher";
        Log.Add(new NativeLogger());
        Log.Add(new FileStreamLogger(SettingsService.LogFilePath));
        SettingsService.Save();
        Settings settings = SettingsService.Load();
        if (settings == null)
            return;

        LocalizationService.Init(settings.LauncherPath, Language.ENGLISH);
        NotificationService.OnNotificationAdded(
            (Notification notification) =>
            {
                Log.Base(notification.LogLevel, notification.Message);
            }
        );
        NotificationService.Log(NativeLogLevel.Info, "log.initialized");
        RequestDataService.OnRequestCompleted(
            (RequestData requestData) =>
            {
                NotificationService.Log(NativeLogLevel.Info, "request.get.success", requestData.URL);
            }
        );

        Request.JsonSerializerOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        Request.HttpClientTimeOut = TimeSpan.FromMinutes(1);
        Watermark.Draw(SettingsService.WatermarkText);

        SevenZipService.Init(settings.SevenZipSettings);
        ModdingService.Init(settings.LauncherPath, settings.ModSettings);

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
        commands.Add(new LaunchPaperServer());
        commands.Add(new DeployMods());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, settings);
    }
}
