using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.MinecraftFabric;

public static class MinecraftFabricPathResolver
{
    public static string FabricInstallerPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.FabricInstallerPath, "installers");

    public static string FabricModPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.FabricInstallerPath, "mods");

    public static string FabricModCategoryPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(FabricModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedFabricInstallerPath(
        MCLauncherPath launcherPath,
        MCFabricInstaller fabricInstaller
    ) => VFS.Combine(launcherPath.FabricInstallerPath, "installers", $"fabric-installer-{fabricInstaller.Version}.jar");

    public static string DownloadedFabricIndexPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.FabricInstallerPath, "fabric_manifest.json");

    public static string DownloadedFabricProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(
            launcherPath.FabricInstallerPath,
            $"fabric_profile-{launcherVersion.Version}-{launcherVersion.FabricLoaderVersion}.json"
        );

    public static string FabricLoaderJarUrlPath(
        MCFabricConfigUrls fabricConfigUrls,
        MCLauncherVersion launcherVersion
    ) => fabricConfigUrls.FabricLoaderJarUrl.Replace("{0}", launcherVersion.FabricLoaderVersion);

    public static string FabricLoaderProfileUrlPath(
        MCFabricConfigUrls fabricConfigUrls,
        MCLauncherVersion launcherVersion
    ) =>
        string.Format(
            fabricConfigUrls.FabricLoaderProfileUrl,
            launcherVersion.Version,
            launcherVersion.FabricLoaderVersion
        );
}
