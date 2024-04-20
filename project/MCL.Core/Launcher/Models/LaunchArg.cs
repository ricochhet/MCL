namespace MCL.Core.Launcher.Models;

public class LaunchArg(string arg, string[] argParams = null, int priority = 0)
{
    public string Arg { get; set; } = arg;
    public string[] ArgParams { get; set; } = argParams;
    public int Priority { get; set; } = priority;
    public bool Ignore { get; set; } = false;

    public string Parse()
    {
        if (ArgParams == null || ArgParams.Length <= 0)
            return Arg;

        return string.Format(Arg, ArgParams);
    }
}
