using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Resolvers.Paper;

public static class PaperPathResolver
{
    public static string InstallerPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperInstallerPath, launcherVersion.Version);

    public static string DownloadedIndexPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.FromCwd(launcherPath.PaperInstallerPath, launcherVersion.Version, "paper_manifest.json");

    public static string DownloadedJarPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(
            InstallerPath(launcherPath, launcherVersion),
            $"paper-{launcherVersion.Version}-{launcherVersion.PaperServerVersion}.jar"
        );
}
