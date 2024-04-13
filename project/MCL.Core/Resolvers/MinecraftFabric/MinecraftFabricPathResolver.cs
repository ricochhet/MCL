using MCL.Core.Interfaces.Resolvers.MinecraftFabric;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.MinecraftFabric;

public class MinecraftFabricPathResolver : IMinecraftFabricPathResolver<MCFabricConfigUrls>
{
    public static string InstallerPath(MCLauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricInstallerPath, "installers");

    public static string ModPath(MCLauncherPath launcherPath) => VFS.FromCwd(launcherPath.FabricInstallerPath, "mods");

    public static string ModCategoryPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.Combine(ModPath(launcherPath), launcherVersion.Version);

    public static string DownloadedInstallerPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricInstallerPath,
            "installers",
            $"fabric-installer-{launcherVersion.FabricInstallerVersion}.jar"
        );

    public static string DownloadedIndexPath(MCLauncherPath launcherPath) =>
        VFS.FromCwd(launcherPath.FabricInstallerPath, "fabric_manifest.json");

    public static string DownloadedProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion) =>
        VFS.FromCwd(
            launcherPath.FabricInstallerPath,
            $"fabric_profile-{launcherVersion.Version}-{launcherVersion.FabricLoaderVersion}.json"
        );

    public static string LoaderJarUrlPath(MCFabricConfigUrls fabricConfigUrls, MCLauncherVersion launcherVersion) =>
        fabricConfigUrls.FabricLoaderJarUrl.Replace("{0}", launcherVersion.FabricLoaderVersion);

    public static string LoaderProfileUrlPath(MCFabricConfigUrls fabricConfigUrls, MCLauncherVersion launcherVersion) =>
        string.Format(
            fabricConfigUrls.FabricLoaderProfileUrl,
            launcherVersion.Version,
            launcherVersion.FabricLoaderVersion
        );
}
