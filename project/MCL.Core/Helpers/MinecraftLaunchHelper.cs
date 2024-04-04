using System.Diagnostics;
using System.IO;

namespace MCL.Core.Helpers;

public class MinecraftLaunchHelper(MinecraftArgHelper minecraftArgHelper)
{
    public void Launch(string minecraftPath)
    {
        ProcessHelper.RunProcess("java", minecraftArgHelper.Build(), minecraftPath, false);
    }
}
