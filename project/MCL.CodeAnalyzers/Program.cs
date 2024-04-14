using System;
using MCL.CodeAnalyzers.Analyzers;
using MCL.Core.Enums.Services;
using MCL.Core.Helpers.Launcher;
using MCL.Core.Logger;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Services;
using MCL.Core.Services.Launcher;

namespace MCL.CodeAnalyzers;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "MCL.CodeAnalyzers";
        Log.Add(new NativeLogger());
        MCLauncherPath launcherPath =
            new(
                path: string.Empty,
                modPath: string.Empty,
                fabricInstallerPath: string.Empty,
                quiltInstallerPath: string.Empty,
                languageLocalizationPath: LaunchPathHelper.LanguageLocalizationPath
            );
        LocalizationService.Init(launcherPath, Language.ENGLISH);
        NotificationService.Init(
            (Notification notification) =>
            {
                Log.Base(notification.LogLevel, notification.Message);
            }
        );
        NotificationService.Add(new Notification(NativeLogLevel.Info, "log.initialized"));
        Watermark.Draw(ConfigService.WatermarkText);

        if (args.Length <= 0)
            return;

        CommandLine.ProcessArgument(
            args,
            "--analyze",
            (string value) =>
            {
                NamespaceAnalyzer.Analyze(value);
                LocalizationKeyAnalyzer.Analyze(value, LocalizationService.Localization);
            }
        );
    }
}
