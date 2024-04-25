using System.Collections.Generic;
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon.Helpers;

public static class CommandHelper
{
    public static List<Command> Commands { get; private set; } = [];

    public static void Add(Command command)
    {
        Commands.Add(command);
    }
}
