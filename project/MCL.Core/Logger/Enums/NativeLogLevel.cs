using System;

namespace MCL.Core.Logger.Enums;

public enum NativeLogLevel
{
    Benchmark = ConsoleColor.Gray,
    Debug = ConsoleColor.DarkGreen,
    Warn = ConsoleColor.DarkYellow,
    Error = ConsoleColor.DarkRed,
    Info = ConsoleColor.DarkCyan,
    Native = ConsoleColor.Magenta
}
