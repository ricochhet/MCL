using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCL.CodeAnalyzers.Commands;
using MCL.Core.Enums.Services;
using MCL.Core.Interfaces.MiniCommon;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
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
        Watermark.Draw(ConfigService.WatermarkText);

        if (args.Length <= 0)
            return;

        List<ILauncherCommand> commands = [];
        commands.Add(new AnalyzeCode());

        foreach (ILauncherCommand command in commands)
            await command.Init(args, config);
    }
}
