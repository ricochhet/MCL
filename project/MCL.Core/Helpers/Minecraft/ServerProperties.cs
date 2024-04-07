using MCL.Core.Models.Launcher;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ServerProperties
{
    public static void NewEula(MCLauncherPath minecraftPath)
    {
        FsProvider.WriteFile(MinecraftPathResolver.ServerPath(minecraftPath), "eula.txt", "eula=true\n");
    }

    public static void NewProperties(MCLauncherPath minecraftPath)
    {
        FsProvider.WriteFile(
            MinecraftPathResolver.ServerPath(minecraftPath),
            "server.properties",
            "online-mode=false\n"
        );
    }
}
