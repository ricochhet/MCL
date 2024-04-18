using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Resolvers.ModLoaders.Fabric;

public static class FabricPathResolver
{
    public static string InstallerPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricInstallerPath, "installers");

    public static string ModPath(LauncherPath launcherPath) => VFS.FromCwd(launcherPath.FabricInstallerPath, "mods");

    public static string ModCategoryPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedInstallerPath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricInstallerPath,
            "installers",
            $"fabric-installer-{launcherVersion.FabricInstallerVersion}.jar"
        );

    public static string DownloadedIndexPath(LauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricInstallerPath, "fabric_manifest.json");

    public static string DownloadedProfilePath(LauncherPath launcherPath, LauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricInstallerPath,
            $"fabric_profile-{launcherVersion.Version}-{launcherVersion.FabricLoaderVersion}.json"
        );

    public static string LoaderJarUrlPath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        fabricUrls.FabricLoaderJarUrl.Replace("{0}", launcherVersion.FabricLoaderVersion);

    public static string LoaderProfileUrlPath(FabricUrls fabricUrls, LauncherVersion launcherVersion) =>
        string.Format(fabricUrls.FabricLoaderProfileUrl, launcherVersion.Version, launcherVersion.FabricLoaderVersion);
}
