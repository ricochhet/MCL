using System.IO;

namespace MCL.Core.MiniCommon;

public static class MinecraftServerProperties
{
    public static void NewEula(string minecraftPath)
    {
        FsProvider.WriteFile(MinecraftPath.ServerPath(minecraftPath), "eula.txt", "eula=true\n");
    }

    public static void NewProperties(string minecraftPath)
    {
        FsProvider.WriteFile(MinecraftPath.ServerPath(minecraftPath), "server.properties", "online-mode=false\n");
    }
}
