using System;
using System.Collections.Generic;
using MCL.Core.Models.Services;

namespace MCL.Core.Services;

public static class NotificationService
{
    private static List<Notification> Notifications { get; set; } = [];
    private static int MaxSize { get; set; } = 100;

    public static void SetMaxSize(int size) => MaxSize = size;

    public static void Add(Notification item) => Notifications.Add(item);

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
