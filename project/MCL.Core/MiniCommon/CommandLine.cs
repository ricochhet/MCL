using System;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;
using MCL.Core.Services.Launcher;

namespace MCL.Core.MiniCommon;

public static class CommandLine
{
    public static void ProcessArgument<T>(string[] args, string flag, Action<T> action)
    {
        try
        {
            int index = Array.IndexOf(args, flag);
            if (index != -1)
            {
                T value = (T)Convert.ChangeType(args[index + 1], typeof(T));
                action(value);
            }
        }
        catch (Exception ex)
        {
            NotificationService.Add(
                new(
                    NativeLogLevel.Error,
                    "log.stack.trace",
                    [ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                )
            );
        }
    }

    public static void ProcessArgument(string[] args, string flag, Action action)
    {
        try
        {
            int index = Array.IndexOf(args, flag);
            if (index != -1)
            {
                action();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Add(
                new(
                    NativeLogLevel.Error,
                    "log.stack.trace",
                    [ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                )
            );
        }
    }

    public static async Task ProcessArgumentAsync<T>(string[] args, string flag, Func<T, Task> action)
    {
        try
        {
            int index = Array.IndexOf(args, flag);
            if (index != -1)
            {
                T value = (T)Convert.ChangeType(args[index + 1], typeof(T));
                await action(value);
            }
        }
        catch (Exception ex)
        {
            NotificationService.Add(
                new(
                    NativeLogLevel.Error,
                    "log.stack.trace",
                    [ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                )
            );
        }
    }

    public static async Task ProcessArgumentAsync(string[] args, string flag, Func<Task> action)
    {
        try
        {
            int index = Array.IndexOf(args, flag);
            if (index != -1)
            {
                await action();
            }
        }
        catch (Exception ex)
        {
            NotificationService.Add(
                new(
                    NativeLogLevel.Error,
                    "log.stack.trace",
                    [ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
                )
            );
        }
    }

    public static void Pause()
    {
        NotificationService.Add(new(NativeLogLevel.Info, "commandline.exit", ["F"]));

#pragma warning disable IDE0079
#pragma warning disable S108
        while (Console.ReadKey(intercept: true).Key != ConsoleKey.F) { }
#pragma warning restore
    }
}
