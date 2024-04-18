using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Modding;

public static class ModPathResolver
{
    public static string ModPath(LauncherPath launcherPath, string modStoreName) =>
        VFS.FromCwd(launcherPath.ModPath, modStoreName);

    public static string ModStorePath(LauncherPath launcherPath, string modStoreName) =>
        VFS.FromCwd(launcherPath.ModPath, $"{modStoreName}.modstore.json");

    public static string ModDeployPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "mods");
}
