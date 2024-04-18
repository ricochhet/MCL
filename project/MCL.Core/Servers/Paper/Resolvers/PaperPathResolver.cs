using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Servers.Paper.Resolvers;

public static class PaperPathResolver
{
    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperPath, launcherVersion.Version);

    public static string VersionManifestPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperPath, launcherVersion.Version, "paper_manifest.json");

    public static string JarPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            InstallerPath(launcherPath, launcherVersion),
            $"paper-{launcherVersion.Version}-{launcherVersion.PaperServerVersion}.jar"
        );
}
