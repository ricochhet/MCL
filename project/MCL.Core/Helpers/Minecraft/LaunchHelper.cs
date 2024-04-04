using MCL.Core.Config;
using MCL.Core.Config.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class LaunchHelper
{
    public static void Launch(string minecraftArgs, string minecraftPath)
    {
        ProcessHelper.RunProcess("java", minecraftArgs, minecraftPath, false);
    }
}
