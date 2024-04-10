using System.Threading.Tasks;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger;

public interface ILogger
{
    public Task Base(NativeLogLevel level, string message);
    public Task Base(NativeLogLevel level, string format, params object[] args);

    public Task Debug(string message);
    public Task Debug(string format, params object[] args);

    public Task Info(string message);
    public Task Info(string format, params object[] args);

    public Task Warn(string message);
    public Task Warn(string format, params object[] args);

    public Task Native(string message);
    public Task Native(string format, params object[] args);

    public Task Error(string message);
    public Task Error(string format, params object[] args);

    public Task Benchmark(string message);
    public Task Benchmark(string format, params object[] args);
}
