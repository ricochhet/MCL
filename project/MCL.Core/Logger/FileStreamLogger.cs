using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Providers;

namespace MCL.Core.Logger;

public class FileStreamLogger : ILogger, IDisposable
{
    private static readonly SemaphoreSlim Semaphore = new(1);
    private static readonly FileStream Stream = VFS.OpenWrite(ConfigProvider.LogFilePath);

    public Task Debug(string message) => WriteToBuffer(FileStreamLogLevel.Debug, message);

    public Task Debug(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Debug, string.Format(format, args));

    public Task Info(string message) => WriteToBuffer(FileStreamLogLevel.Info, message);

    public Task Info(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Info, string.Format(format, args));

    public Task Warn(string message) => WriteToBuffer(FileStreamLogLevel.Warn, message);

    public Task Warn(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Warn, string.Format(format, args));

    public Task Native(string message) => WriteToBuffer(FileStreamLogLevel.Info, message);

    public Task Native(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Info, string.Format(format, args));

    public Task Error(string message) => WriteToBuffer(FileStreamLogLevel.Error, message);

    public Task Error(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Error, string.Format(format, args));

    public Task Benchmark(string message) => WriteToBuffer(FileStreamLogLevel.Info, message);

    public Task Benchmark(string format, params object[] args) =>
        WriteToBuffer(FileStreamLogLevel.Info, string.Format(format, args));

    private static async Task WriteToBuffer(FileStreamLogLevel level, string message)
    {
        try
        {
            await Semaphore.WaitAsync();
            await Stream.WriteAsync(
                Encoding.UTF8.GetBytes($"[{DateTime.Now.ToLongTimeString()}][{level}] {message}\n")
            );
            await Stream.FlushAsync();
        }
        catch { }
        finally
        {
            Semaphore.Release();
        }
    }

    public void Dispose()
    {
        try
        {
            GC.SuppressFinalize(this);
            Semaphore.Wait();
            Stream.Dispose();
        }
        catch { }
        finally
        {
            Semaphore.Release();
        }
    }
}
