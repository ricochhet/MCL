using System.IO;

namespace MCL.Core.MiniCommon;

public static class MinecraftServerProperties
{
    public static void NewEula(string minecraftPath)
    {
        string serverPath = Path.Combine(minecraftPath, "server");
        FsProvider.WriteFile(serverPath, "eula.txt", "eula=true\n");
    }

    public static void NewProperties(string minecraftPath)
    {
        string serverPath = Path.Combine(minecraftPath, "server");
        FsProvider.WriteFile(serverPath, "server.properties", "online-mode=false\n");
    }
}