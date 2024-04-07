using System.Collections.Generic;

namespace MCL.Core.Models;

public class LaunchArg(string arg, List<string> argParams = null, int priority = 0)
{
    public string Arg { get; set; } = arg;
    public List<string> ArgParams { get; set; } = argParams;
    public int Priority { get; set; } = priority;

    private string currentArg = arg;
    private int currentParam = 0;

    public string Parse()
    {
        if (ArgParams == null || ArgParams.Count <= 0)
            return Arg;

        foreach (string param in ArgParams)
        {
            currentArg = currentArg.Replace("{" + currentParam + "}", param);
            currentParam++;
        }

        return currentArg;
    }
}
