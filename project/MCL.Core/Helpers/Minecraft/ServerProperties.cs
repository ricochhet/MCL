using MCL.Core.Models.Launcher;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ServerProperties
{
    public static void NewEula(MCLauncherPath launcherPath)
    {
        FsProvider.WriteFile(MinecraftPathResolver.ServerPath(launcherPath), "eula.txt", "eula=true\n");
    }

    public static void NewProperties(MCLauncherPath launcherPath)
    {
        FsProvider.WriteFile(
            MinecraftPathResolver.ServerPath(launcherPath),
            "server.properties",
            "online-mode=false\n"
        );
    }
}
