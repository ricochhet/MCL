using MCL.Core.Interfaces.Resolvers.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Resolvers.MinecraftQuilt;

public class MinecraftQuiltPathResolver : IMinecraftFabricPathResolver<MCQuiltConfigUrls>
{
    public static string InstallerPath(MCLauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltInstallerPath, "installers");

    public static string ModPath(MCLauncherPath launcherPath) => VFS.FromCwd(launcherPath.QuiltInstallerPath, "mods");

    public static string ModCategoryPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedInstallerPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.QuiltInstallerPath,
            "installers",
            $"quilt-installer-{launcherVersion.QuiltInstallerVersion}.jar"
        );

    public static string DownloadedIndexPath(MCLauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.QuiltInstallerPath, "quilt_manifest.json");

    public static string DownloadedProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.QuiltInstallerPath,
            $"quilt_profile-{launcherVersion.Version}-{launcherVersion.QuiltLoaderVersion}.json"
        );

    public static string LoaderJarUrlPath(MCQuiltConfigUrls quiltConfigUrls, MCLauncherVersion launcherVersion) =>
        quiltConfigUrls.QuiltLoaderJarUrl.Replace("{0}", launcherVersion.QuiltLoaderVersion);

    public static string LoaderProfileUrlPath(MCQuiltConfigUrls quiltConfigUrls, MCLauncherVersion launcherVersion) =>
        string.Format(
            quiltConfigUrls.QuiltLoaderProfileUrl,
            launcherVersion.Version,
            launcherVersion.QuiltLoaderVersion
        );
}
