using System.Linq;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Models;

namespace MCL.Core.Launcher.Extensions;

public static class MArgumentExt
{
    public static JvmArguments? JvmArguments(this MArgument[] arguments)
    {
        return new JvmArguments { Arguments = arguments.Where(a => a.Condition).ToList() };
    }
}
