using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger;

public class NativeLogger : ILogger
{
    [DllImport("kernel32", SetLastError = true)]
    private static extern bool AllocConsole();

    public NativeLogger()
    {
        _ = AllocConsole();
    }

    public Task Benchmark(string message) => WriteToStdout(NativeLogLevel.Benchmark, message);

    public Task Benchmark(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Benchmark, string.Format(format, args));

    public Task Debug(string message) => WriteToStdout(NativeLogLevel.Debug, message);

    public Task Debug(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Debug, string.Format(format, args));

    public Task Info(string message) => WriteToStdout(NativeLogLevel.Info, message);

    public Task Info(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Info, string.Format(format, args));

    public Task Warn(string message) => WriteToStdout(NativeLogLevel.Warn, message);

    public Task Warn(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Warn, string.Format(format, args));

    public Task Error(string message) => WriteToStdout(NativeLogLevel.Error, message);

    public Task Error(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Error, string.Format(format, args));

    public Task Native(string message) => WriteToStdout(NativeLogLevel.Error, message);

    public Task Native(string format, params object[] args) =>
        WriteToStdout(NativeLogLevel.Native, string.Format(format, args));

    private static Task WriteToStdout(NativeLogLevel level, string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write($"[{DateTime.Now.ToLongTimeString()}]");
        Console.ForegroundColor = (ConsoleColor)level;
        Console.Write($"[{level.ToString().ToUpper()}] ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);

        return Task.FromResult(true);
    }
}
