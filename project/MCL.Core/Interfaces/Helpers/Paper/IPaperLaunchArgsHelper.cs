using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Helpers.Minecraft;

public interface IPaperLaunchArgsHelper
{
    public static abstract JvmArguments Default(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion);
}
