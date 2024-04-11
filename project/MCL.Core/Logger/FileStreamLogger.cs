using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Services;

namespace MCL.Core.Logger;

public class FileStreamLogger : ILogger, IDisposable
{
    private static readonly SemaphoreSlim Semaphore = new(1);
    private static readonly FileStream Stream = VFS.OpenWrite(ConfigService.LogFilePath);

    public Task Base(NativeLogLevel level, string message) => WriteToBuffer(level, message);

    public Task Base(NativeLogLevel level, string format, params object[] args) =>
        WriteToBuffer(level, string.Format(format, args));

    public Task Debug(string message) => WriteToBuffer(NativeLogLevel.Debug, message);

    public Task Debug(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Debug, string.Format(format, args));

    public Task Info(string message) => WriteToBuffer(NativeLogLevel.Info, message);

    public Task Info(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Info, string.Format(format, args));

    public Task Warn(string message) => WriteToBuffer(NativeLogLevel.Warn, message);

    public Task Warn(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Warn, string.Format(format, args));

    public Task Native(string message) => WriteToBuffer(NativeLogLevel.Info, message);

    public Task Native(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Info, string.Format(format, args));

    public Task Error(string message) => WriteToBuffer(NativeLogLevel.Error, message);

    public Task Error(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Error, string.Format(format, args));

    public Task Benchmark(string message) => WriteToBuffer(NativeLogLevel.Info, message);

    public Task Benchmark(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Info, string.Format(format, args));

    private static async Task WriteToBuffer(NativeLogLevel level, string message)
    {
        try
        {
            await Semaphore.WaitAsync();
            await Stream.WriteAsync(
                Encoding.UTF8.GetBytes($"[{DateTime.Now.ToLongTimeString()}][{level}] {message}\n")
            );
            await Stream.FlushAsync();
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        try
        {
            Semaphore.Wait();
            Stream.Dispose();
        }
        finally
        {
            Semaphore.Release();
        }
    }

    ~FileStreamLogger()
    {
        Dispose(false);
    }
}
