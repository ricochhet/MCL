using System;
using System.Collections.Generic;
using MCL.Core.Launcher.Models;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Launcher.Services;

public static class NotificationService
{
    private static readonly List<Notification> _notifications = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(Notification item) => _notifications.Add(item);

    public static void Log(NativeLogLevel level, string id)
    {
        _notifications.Add(new Notification(level, id));
    }

    public static void Log(NativeLogLevel level, string id, string[] _params)
    {
        _notifications.Add(new Notification(level, id, _params));
    }

    public static void Clear() => _notifications.Clear();

    public static void Init(Action<Notification> func)
    {
        Notification.OnNotificationAdded += func;
        Notification.OnNotificationAdded += Manage;
    }

    private static void Manage(Notification _)
    {
        if (_notifications.Count > MaxSize)
            _notifications.RemoveAt(0);
    }
}
