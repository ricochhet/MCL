using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Resolvers;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Helpers;

public static class ServerProperties
{
    public static void NewEula(LauncherPath launcherPath)
    {
        VFS.WriteFile(VFS.Combine(MPathResolver.ServerPath(launcherPath), "eula.txt"), "eula=true\n");
    }

    public static void NewProperties(LauncherPath launcherPath)
    {
        VFS.WriteFile(VFS.Combine(MPathResolver.ServerPath(launcherPath), "server.properties"), "online-mode=false\n");
    }
}
