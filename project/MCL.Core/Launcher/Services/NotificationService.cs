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

    public static void Benchmark(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Benchmark, "log", _params));

    public static void Debug(params string[] _params) => _notifications.Add(new(NativeLogLevel.Debug, "log", _params));

    public static void Warn(params string[] _params) => _notifications.Add(new(NativeLogLevel.Warn, "log", _params));

    public static void Error(params string[] _params) => _notifications.Add(new(NativeLogLevel.Error, "log", _params));

    public static void Info(params string[] _params) => _notifications.Add(new(NativeLogLevel.Info, "log", _params));

    public static void Native(params string[] _params) =>
        _notifications.Add(new(NativeLogLevel.Native, "log", _params));

    public static void Print(NativeLogLevel level, params string[] _params) =>
        _notifications.Add(new(level, "log", _params));

    public static void Log(NativeLogLevel level, string id) => _notifications.Add(new(level, id));

    public static void Log(NativeLogLevel level, string id, params string[] _params) =>
        _notifications.Add(new(level, id, _params));

    public static void Clear() => _notifications.Clear();

    public static void OnNotificationAdded(Action<Notification> func)
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
