using System;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;
using MCL.Core.Models.Services;
using MCL.Core.Services;

namespace MCL.Core.MiniCommon;

public static class CommandLine
{
    public static void ProcessArgument<T>(string[] args, string flag, Action<T> action)
    {
        int index = Array.IndexOf(args, flag);
        if (index != -1)
        {
            T value = (T)Convert.ChangeType(args[index + 1], typeof(T));
            action(value);
        }
    }

    public static void ProcessArgument(string[] args, string flag, Action action)
    {
        int index = Array.IndexOf(args, flag);
        if (index != -1)
        {
            action();
        }
    }

    public static async Task ProcessArgumentAsync<T>(string[] args, string flag, Func<T, Task> action)
    {
        int index = Array.IndexOf(args, flag);
        if (index != -1)
        {
            T value = (T)Convert.ChangeType(args[index + 1], typeof(T));
            await action(value);
        }
    }

    public static async Task ProcessArgumentAsync(string[] args, string flag, Func<Task> action)
    {
        int index = Array.IndexOf(args, flag);
        if (index != -1)
        {
            await action();
        }
    }

    public static void Pause()
    {
        NotificationService.Add(new Notification(NativeLogLevel.Info, "commandline.exit", ["F"]));

#pragma warning disable IDE0079
#pragma warning disable S108
        while (Console.ReadKey(intercept: true).Key != ConsoleKey.F) { }
#pragma warning restore
    }
}
