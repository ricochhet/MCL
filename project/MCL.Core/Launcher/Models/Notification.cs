using System;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Launcher.Models;

public class Notification
{
    public DateTime CurrentDateTime { get; set; } = DateTime.Now;
    public NativeLogLevel LogLevel { get; set; }
    public string ID { get; set; }
    public string Message { get; set; }
    public string[] Params { get; set; }
    public static event Action<Notification> OnNotificationAdded;
#nullable enable
    public Exception? Exception { get; set; }

    public Notification(NativeLogLevel logLevel, string id, Exception? exception = null)
    {
        LogLevel = logLevel;
        ID = id;
        Message = LocalizationService.Translate(ID);
        Exception = exception;
        OnNotificationAdded?.Invoke(this);
    }

    public Notification(NativeLogLevel logLevel, string id, string[] _params, Exception? exception = null)
#nullable disable
    {
        LogLevel = logLevel;
        ID = id;
        Params = _params;
        Message = string.Format(LocalizationService.Translate(ID), Params);
        Exception = exception;
        OnNotificationAdded?.Invoke(this);
    }
}
