using System;
using System.Collections.Generic;
using MCL.Core.Models.Services;

namespace MCL.Core.Services;

public static class NotificationService
{
    public static List<Notification> Notifications { get; private set; }

    static NotificationService()
    {
        Notifications = [];
    }

    public static void Add(Notification item) => Notifications.Add(item);
    public static void LogNotification(Action<Notification> func)
    {
        Notification.OnNotificationAdded += func;
    }
}
