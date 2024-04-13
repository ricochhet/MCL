using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Modding;

public static class ModPathResolver
{
    public static string ModPath(MCLauncherPath launcherPath, string modStoreName) =>
        VFS.FromCwd(launcherPath.ModPath, modStoreName);

    public static string ModStorePath(MCLauncherPath launcherPath, string modStoreName) =>
        VFS.FromCwd(launcherPath.ModPath, $"{modStoreName}.modstore.json");
}
