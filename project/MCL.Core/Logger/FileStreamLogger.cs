using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.Logger.Models;

namespace MCL.Core.Logger;

public class FileStreamLogger : ILogger, IDisposable
{
    private readonly Queue<FileStreamLog> _queue;
    private readonly StreamWriter _stream;
    private readonly object _mutex = new();
    private readonly bool _flush = true;
    private bool _disposed = false;
    private readonly NativeLogLevel _minLevel = NativeLogLevel.Debug;

    public FileStreamLogger(string filePath)
    {
        _queue = new Queue<FileStreamLog>();
        _stream = new StreamWriter(filePath, append: true);
        Task.Run(Flush);
    }

    public FileStreamLogger(string filePath, NativeLogLevel minLevel)
    {
        _queue = new Queue<FileStreamLog>();
        _stream = new StreamWriter(filePath, append: true);
        _minLevel = minLevel;
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
        lock (_mutex)
        {
            if ((int)level < (int)_minLevel)
                return Task.FromResult(true);
            _queue.Enqueue(new FileStreamLog(level, message));
            Monitor.Pulse(_mutex);
        }
        return Task.FromResult(true);
    }

    private async Task Flush()
    {
        while (_flush)
        {
            FileStreamLog[] messages;
            lock (_mutex)
            {
                messages = [.. _queue];
                _queue.Clear();
            }

            await WriteToStream(messages);

            lock (_mutex)
            {
                Monitor.Wait(_mutex, TimeSpan.FromSeconds(1));
            }
        }
    }

    private async Task WriteToStream(FileStreamLog[] messages)
    {
        try
        {
            foreach (FileStreamLog message in messages)
            {
                await _stream.WriteLineAsync(
                    $"[{DateTime.Now.ToLongTimeString()}][{message.Level}] {message.Message}\n"
                );
            }
            await _stream.FlushAsync();
        }
        catch (Exception ex)
        {
            NotificationService.Error("error.writefile", ex.Message);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _stream.Dispose();
            _disposed = true;
        }
    }

    ~FileStreamLogger()
    {
        Dispose(false);
    }
}
