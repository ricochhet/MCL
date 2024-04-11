using System;
using MCL.Core.Logger.Enums;
using MCL.Core.Services;

namespace MCL.Core.Models.Services;

public class Notification
{
    public DateTime CurrentDateTime { get; set; } = DateTime.Now;
    public NativeLogLevel LogLevel { get; set; }
    public string ID { get; set; }
    public string Message { get; set; }
    public static event Action<Notification> OnNotificationAdded;
#nullable enable
    public Exception? Exception { get; set; }

    public Notification(NativeLogLevel _logLevel, string _ID, Exception? _exception = null)
#nullable disable
    {
        LogLevel = _logLevel;
        ID = _ID;
        Message = LocalizationService.Translate(ID);
        Exception = _exception;
        OnNotificationAdded?.Invoke(this);
    }
}
