using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger.Models;

public class FileStreamLog(NativeLogLevel level, string message)
{
    public NativeLogLevel Level { get; set; } = level;
    public string Message { get; set; } = message;
}
