using System;
using MCL.Core.Logger.Enums;
using MCL.Core.Services;

namespace MCL.Core.Models.Services;

public class Notification
{
    public DateTime CurrentDateTime { get; set; }
    public FileStreamLogLevel LogLevel { get; set; }
    public string ID { get; set; }
    public string Message { get; private set; }

    public Notification(FileStreamLogLevel _logLevel, string _ID)
    {
        CurrentDateTime = DateTime.Now;
        LogLevel = _logLevel;
        ID = _ID;
        Message = TranslationService.Translate(ID);
    }
}
