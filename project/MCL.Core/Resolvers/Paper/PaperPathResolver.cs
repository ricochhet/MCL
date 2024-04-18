using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Paper;

public static class PaperPathResolver
{
    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperInstallerPath, launcherVersion.Version);

    public static string DownloadedIndexPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperInstallerPath, launcherVersion.Version, "paper_manifest.json");

    public static string DownloadedJarPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            InstallerPath(launcherPath, launcherVersion),
            $"paper-{launcherVersion.Version}-{launcherVersion.PaperServerVersion}.jar"
        );
}
