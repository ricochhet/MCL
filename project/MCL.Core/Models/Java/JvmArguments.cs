using System.Collections.Generic;

namespace MCL.Core.Models.Java;

public class JvmArguments
{
    private readonly List<string> parsedLaunchArgs = [];

    public void Add(LaunchArg launchArg)
    {
        parsedLaunchArgs.Add(launchArg.Parse());
    }

    public string Build()
    {
        return string.Join(" ", parsedLaunchArgs);
    }
}
