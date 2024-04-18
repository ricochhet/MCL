using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public static class QuiltPathResolver
{
    public static string InstallerPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltInstallerPath, "installers");

    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.QuiltInstallerPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedInstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltInstallerPath,
            "installers",
            $"quilt-installer-{launcherVersion.QuiltInstallerVersion}.jar"
        );

    public static string DownloadedIndexPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltInstallerPath, "quilt_manifest.json");

    public static string DownloadedProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.QuiltInstallerPath,
            $"quilt_profile-{launcherVersion.Version}-{launcherVersion.QuiltLoaderVersion}.json"
        );

    public static string LoaderJarUrlPath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        quiltUrls.QuiltLoaderJarUrl.Replace("{0}", launcherVersion.QuiltLoaderVersion);

    public static string LoaderProfileUrlPath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        string.Format(quiltUrls.QuiltLoaderProfileUrl, launcherVersion.Version, launcherVersion.QuiltLoaderVersion);
}
