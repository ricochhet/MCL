using System.Collections.Generic;
using System.Linq;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Models.Java;

public class JvmArguments
{
    public List<LaunchArg> Arguments { get; set; } = [];
    private readonly List<string> parsedLaunchArgs = [];

    public void Add(string arg, string[] argParams = null, int priority = 0) =>
        Arguments.Add(new(arg, argParams, priority));

    public string Build()
    {
        parsedLaunchArgs.Clear();
        List<LaunchArg> sortedLaunchArgs = [.. Arguments.OrderBy(a => a.Priority)];
        foreach (LaunchArg arg in sortedLaunchArgs)
        {
            parsedLaunchArgs.Add(arg.Parse());
        }
        return string.Join(" ", parsedLaunchArgs);
    }
}
