using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MCL.Core.Logger.Enums;
using MCL.Core.Logger.Models;

namespace MCL.Core.Logger;

public class FileStreamLogger : ILogger, IDisposable
{
    private readonly Queue<FileStreamLog> queue;
    private readonly StreamWriter stream;
    private readonly object mutex = new();
    private readonly bool flush = true;
    private bool disposed = false;

    public FileStreamLogger(string filePath)
    {
        queue = new Queue<FileStreamLog>();
        stream = new StreamWriter(filePath, append: true);
        Task.Run(Flush);
    }

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

    private Task<bool> WriteToBuffer(NativeLogLevel level, string message)
    {
        lock (mutex)
        {
            queue.Enqueue(new FileStreamLog(level, message));
            Monitor.Pulse(mutex);
        }
        return Task.FromResult(true);
    }

    private async Task Flush()
    {
        while (flush)
        {
            FileStreamLog[] messages;
            lock (mutex)
            {
                messages = [.. queue];
                queue.Clear();
            }

            await WriteToStream(messages);

            lock (mutex)
            {
                Monitor.Wait(mutex, TimeSpan.FromSeconds(1));
            }
        }
    }

    private async Task WriteToStream(FileStreamLog[] messages)
    {
        try
        {
            foreach (FileStreamLog message in messages)
            {
                await stream.WriteLineAsync(
                    $"[{DateTime.Now.ToLongTimeString()}][{message.Level}] {message.Message}\n"
                );
            }
            await stream.FlushAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
                stream.Dispose();
            disposed = true;
        }
    }

    ~FileStreamLogger()
    {
        Dispose(false);
    }
}
