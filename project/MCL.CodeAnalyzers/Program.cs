using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.CodeAnalyzers.Commands;
using MCL.Core.Enums.Services;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon.Interfaces;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "MCL.CodeAnalyzers";
        Log.Add(new NativeLogger());
        Log.Add(new FileStreamLogger(SettingsService.LogFilePath));
        SettingsService.Save();
        Settings settings = SettingsService.Load();
        if (settings == null)
            return;

        LocalizationService.Init(settings.LauncherPath, Language.ENGLISH);
        NotificationService.Init(
            (Notification notification) =>
            {
                Log.Base(notification.LogLevel, notification.Message);
            }
        );
        NotificationService.Log(NativeLogLevel.Info, "log.initialized");
        Watermark.Draw(SettingsService.WatermarkText);

        if (args.Length <= 0)
            return;

        List<ILauncherCommand> commands = [];
        commands.Add(new AnalyzeCode());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, settings);
    }
}
