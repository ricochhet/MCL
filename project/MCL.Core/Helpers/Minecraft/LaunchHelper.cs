using MCL.Core.Config;
using MCL.Core.Config.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public class LaunchHelper(MinecraftArgConfig minecraftArgConfig)
{
    public void Launch(string minecraftPath)
    {
        ProcessHelper.RunProcess("java", minecraftArgConfig.Build(), minecraftPath, false);
    }
}
