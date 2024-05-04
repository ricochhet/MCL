/*
 * MCL - Minecraft Launcher
 * Copyright (C) 2024 MCL Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MiniCommon.IO;
using MiniCommon.Logger.Enums;
using MiniCommon.Logger.Models;
using MiniCommon.Providers;
using MiniCommon.Validation;
using MiniCommon.Validation.Operators;

namespace MiniCommon.Logger;

public class FileStreamLogger : ILogger, IDisposable
{
    private readonly Queue<FileStreamLog> _queue;
    private readonly StreamWriter _stream;
    private readonly object _mutex = new();
    private readonly bool _flush = true;
    private bool _disposed = false;
    private readonly NativeLogLevel _minLevel = NativeLogLevel.Debug;

    /// <summary>
    /// Create a new FileStreamLogger toward the specified log path.
    /// </summary>
    public FileStreamLogger(string filePath)
    {
        _queue = new();
        VFS.CreateDirectory(VFS.GetDirectoryName(filePath));
        _stream = new(filePath, append: true);
        AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        Task.Run(Flush);
    }

    /// <summary>
    /// Create a new FileStreamLogger with a minimum log level toward the specified log path.
    /// </summary>
    public FileStreamLogger(string filePath, NativeLogLevel minLevel)
    {
        _queue = new();
        VFS.CreateDirectory(VFS.GetDirectoryName(filePath));
        _stream = new(filePath, append: true);
        _minLevel = minLevel;
        AppDomain.CurrentDomain.UnhandledException += UnhandledException;
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

    public Task Fatal(string message) => WriteToBuffer(NativeLogLevel.Fatal, message);

    public Task Fatal(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Fatal, string.Format(format, args));

    public Task Benchmark(string message) => WriteToBuffer(NativeLogLevel.Info, message);

    public Task Benchmark(string format, params object[] args) =>
        WriteToBuffer(NativeLogLevel.Info, string.Format(format, args));

    /// <summary>
    /// Handle incoming exception objects.
    /// </summary>
    private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            NotificationProvider.Error("log.unhandled.exception", ex.ToString());
        }
        else
        {
            NotificationProvider.Error(
                "log.unhandled.object",
                e.ExceptionObject.ToString() ?? Validate.For.EmptyString()
            );
        }
    }

    /// <summary>
    /// Places a message into the queue if the minimum level is lower than the message level.
    /// </summary>
    private Task<bool> WriteToBuffer(NativeLogLevel level, string message)
    {
        lock (_mutex)
        {
            if ((int)level < (int)_minLevel)
                return Task.FromResult(true);
            _queue.Enqueue(new(level, message));
            Monitor.Pulse(_mutex);
        }
        return Task.FromResult(true);
    }

    /// <summary>
    /// Flush the message queue and write to the file stream.
    /// </summary>
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

    /// <summary>
    /// Write all messages to the file stream and flush the stream.
    /// </summary>
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
            NotificationProvider.Error("error.writefile", ex.Message);
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
