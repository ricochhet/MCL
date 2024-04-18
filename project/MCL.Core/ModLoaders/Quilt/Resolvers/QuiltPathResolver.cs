using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Resolvers;

public static class QuiltPathResolver
{
    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.QuiltPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltPath,
            "installers",
            $"quilt-installer-{launcherVersion.QuiltInstallerVersion}.jar"
        );

    public static string VersionManifestPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltPath, "quilt_manifest.json");

    public static string ProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.QuiltPath,
            $"quilt_profile-{launcherVersion.Version}-{launcherVersion.QuiltLoaderVersion}.json"
        );

    public static string LoaderJarPath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        quiltUrls.LoaderJar.Replace("{0}", launcherVersion.QuiltLoaderVersion);

    public static string LoaderProfilePath(QuiltUrls quiltUrls, LauncherVersion launcherVersion) =>
        string.Format(quiltUrls.LoaderProfile, launcherVersion.Version, launcherVersion.QuiltLoaderVersion);
}
