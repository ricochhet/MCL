using System;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger.Models;

public class LogInfo
{
    public DateTime DateTime { get; set; }
    public FileStreamLogLevel LogLevel { get; set; }
    public LogType LogType { get; set; }
}
