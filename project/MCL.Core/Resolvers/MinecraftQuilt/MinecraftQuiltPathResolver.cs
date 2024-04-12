using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Resolvers.MinecraftQuilt;

public static class MinecraftQuiltPathResolver
{
    public static string QuiltInstallerPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.QuiltInstallerPath, "installers");

    public static string QuiltModPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.QuiltInstallerPath, "mods");

    public static string QuiltModCategoryPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(QuiltModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedQuiltInstallerPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltInstallerPath,
            "installers",
            $"quilt-installer-{launcherVersion.QuiltInstallerVersion}.jar"
        );

    public static string DownloadedQuiltIndexPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.QuiltInstallerPath, "quilt_manifest.json");

    public static string DownloadedQuiltProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltInstallerPath,
            $"quilt_profile-{launcherVersion.Version}-{launcherVersion.QuiltLoaderVersion}.json"
        );

    public static string QuiltLoaderJarUrlPath(MCQuiltConfigUrls quiltConfigUrls, MCLauncherVersion launcherVersion) =>
        quiltConfigUrls.QuiltLoaderJarUrl.Replace("{0}", launcherVersion.QuiltLoaderVersion);

    public static string QuiltLoaderProfileUrlPath(
        MCQuiltConfigUrls quiltConfigUrls,
        MCLauncherVersion launcherVersion
    ) =>
        string.Format(
            quiltConfigUrls.QuiltLoaderProfileUrl,
            launcherVersion.Version,
            launcherVersion.QuiltLoaderVersion
        );
}
