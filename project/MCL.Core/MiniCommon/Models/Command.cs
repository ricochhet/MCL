using System.Collections.Generic;
using MCL.Core.MiniCommon.Helpers;

namespace MCL.Core.MiniCommon.Models;

public class Command
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<CommandParameter> Parameters { get; set; } = [];

    public Command()
    {
        CommandHelper.Add(this);
    }
}
