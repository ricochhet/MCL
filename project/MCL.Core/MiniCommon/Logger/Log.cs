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

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using MCL.Core.MiniCommon.Logger.Enums;

namespace MCL.Core.MiniCommon.Logger;

public class Log
{
    private readonly List<ILogger> _io = [];
    private static Log _instance;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly Dictionary<string, Stopwatch> _benchmarkers = [];

    public static Log Instance
    {
        get
        {
            _instance ??= new();

            return _instance;
        }
    }

#pragma warning disable IDE0079
#pragma warning disable S3168
    public static async void Add(ILogger logger)
    {
        try
        {
            await _semaphore.WaitAsync();
            Instance._io.Add(logger);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Base(NativeLogLevel level, string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Base(level, message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Base(NativeLogLevel level, string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Base(level, format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Debug(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Debug(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Debug(string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Debug(format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Native(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Native(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Native(string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Native(format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Info(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Info(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Info(string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Info(format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Warn(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Warn(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Warn(string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Warn(format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Error(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Error(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static async void Error(string format, params object[] args)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Error(format, args);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    private static async void BenchmarkLog(string message)
    {
        try
        {
            await _semaphore.WaitAsync();

            foreach (ILogger logger in Instance._io)
                await logger.Benchmark(message);
        }
        finally
        {
            _ = _semaphore.Release();
        }
    }

    public static void Benchmark([CallerMemberName] string name = "")
    {
        if (Instance._benchmarkers.ContainsKey(name))
            return;

        BenchmarkLog($"Starting benchmark for '{name}'");
        Instance._benchmarkers.Add(name, Stopwatch.StartNew());
    }

    public static void BenchmarkEnd([CallerMemberName] string name = "")
    {
        if (!Instance._benchmarkers.ContainsKey(name))
            return;

        Stopwatch benchmarker = Instance._benchmarkers[name];
        BenchmarkLog($"Time taken for '{name}': {benchmarker.ElapsedMilliseconds}ms");
        benchmarker.Stop();
        _ = Instance._benchmarkers.Remove(name);
    }
#pragma warning restore
}