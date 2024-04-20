using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ServerProperties
{
    public static void NewEula(LauncherPath launcherPath)
    {
        string filepath = VFS.Combine(MPathResolver.ServerPath(launcherPath), "eula.txt");
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "eula=true\n");
    }

    public static void NewProperties(LauncherPath launcherPath)
    {
        string filepath = VFS.Combine(MPathResolver.ServerPath(launcherPath), "server.properties");
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "online-mode=false\n");
    }
}
