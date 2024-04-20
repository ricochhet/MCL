using MCL.Core.Launcher.Models;
using MCL.Core.MiniCommon;
using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Resolvers;

public static class FabricPathResolver
{
    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.FabricPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string InstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.FabricPath,
            "installers",
            $"fabric-installer-{launcherVersion.FabricInstallerVersion}.jar"
        );

    public static string InstallersPath(LauncherPath launcherPath) =>
        VFS.Combine(launcherPath.FabricPath, "installers");

    public static string VersionManifestPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricPath, "fabric_manifest.json");

    public static string ProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricPath,
            $"fabric_profile-{launcherVersion.Version}-{launcherVersion.FabricLoaderVersion}.json"
        );

    public static string LoaderJarPath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        fabricUrls.LoaderJar.Replace("{0}", launcherVersion.FabricLoaderVersion);

    public static string LoaderProfilePath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        string.Format(fabricUrls.LoaderProfile, launcherVersion.Version, launcherVersion.FabricLoaderVersion);
}
