using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger;

public class Log
{
    private readonly List<ILogger> _io = new();
    private static Log _instance;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly Dictionary<string, Stopwatch> _benchmarkers = new();

    public static Log Instance
    {
        get
        {
            _instance ??= new Log();

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
