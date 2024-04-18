using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ServerProperties
{
    public static void NewEula(LauncherPath launcherPath)
    {
        VFS.WriteFile(VFS.Combine(MinecraftPathResolver.ServerPath(launcherPath), "eula.txt"), "eula=true\n");
    }

    public static void NewProperties(LauncherPath launcherPath)
    {
        VFS.WriteFile(
            VFS.Combine(MinecraftPathResolver.ServerPath(launcherPath), "server.properties"),
            "online-mode=false\n"
        );
    }
}
