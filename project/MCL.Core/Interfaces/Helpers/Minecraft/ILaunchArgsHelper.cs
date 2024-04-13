using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Helpers.Minecraft;

public interface ILaunchArgsHelper
{
    public static abstract JvmArguments Default(MCLauncher launcher);
}
