using System.Collections.Generic;

namespace MCL.Core.Models.Java;

public class JvmArguments
{
    public List<LaunchArg> Arguments { get; set; } = [];
    private readonly List<string> parsedLaunchArgs = [];

    public void Add(LaunchArg launchArg)
    {
        Arguments.Add(launchArg);
        parsedLaunchArgs.Add(launchArg.Parse());
    }

    public string Build()
    {
        return string.Join(" ", parsedLaunchArgs);
    }
}
