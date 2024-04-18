using System;
using System.Collections.Generic;
using MCL.Core.Logger.Enums;
using MCL.Core.Models.Services;

namespace MCL.Core.Services.Launcher;

public static class NotificationService
{
    private static List<Notification> Notifications { get; set; } = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(Notification item) => Notifications.Add(item);

    public static void Log(NativeLogLevel level, string id)
    {
        Notifications.Add(new Notification(level, id));
    }

    public static void Log(NativeLogLevel level, string id, string[] _params)
    {
        Notifications.Add(new Notification(level, id, _params));
    }

    public static void Clear() => Notifications.Clear();

    public static void Init(Action<Notification> func)
    {
        Notification.OnNotificationAdded += func;
        Notification.OnNotificationAdded += Manage;
    }

    private static void Manage(Notification _)
    {
        if (Notifications.Count > MaxSize)
            Notifications.RemoveAt(0);
    }
}
