using System;
using System.Collections.Generic;
using MCL.Core.Models.Services;

namespace MCL.Core.Services;

public static class NotificationService
{
    private static List<Notification> Notifications { get; set; }

    static NotificationService()
    {
        Notifications = [];
    }

    public static void Add(Notification item) => Notifications.Add(item);

    public static void Clear() => Notifications.Clear();

    public static void LogNotification(Action<Notification> func)
    {
        Notification.OnNotificationAdded += func;
    }
}
