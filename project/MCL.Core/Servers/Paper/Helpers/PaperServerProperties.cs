using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.Servers.Paper.Resolvers;

namespace MCL.Core.Servers.Paper.Helpers;

public static class PaperServerProperties
{
    public static void NewEula(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        string filepath = VFS.Combine(PaperPathResolver.InstallerPath(launcherPath, launcherVersion), "eula.txt");
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "eula=true\n");
    }

    public static void NewProperties(LauncherPath launcherPath, LauncherVersion launcherVersion)
    {
        string filepath = VFS.Combine(
            PaperPathResolver.InstallerPath(launcherPath, launcherVersion),
            "server.properties"
        );
        if (!VFS.Exists(filepath))
            VFS.WriteFile(filepath, "online-mode=false\n");
    }
}
