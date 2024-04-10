using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftPathResolver
{
    public static string ClientLibrary(MCLauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}.jar").Replace("\\", "/");

    public static string Libraries(MCLauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}-natives").Replace("\\", "/");

    public static string AssetsPath(MCLauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "assets");

    public static string LibraryPath(MCLauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "libraries");

    public static string ServerPath(MCLauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "server");

    public static string VersionPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(launcherPath.Path, "versions", versionDetails.ID);

    public static string ClientJarPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), versionDetails.ID + ".jar");

    public static string ClientMappingsPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.txt");

    public static string ClientIndexPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(AssetsPath(launcherPath), "indexes", versionDetails.Assets + ".json");

    public static string LoggingPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.xml");

    public static string LoggingPath(MCLauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, "client.xml");

    public static string ServerJarPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.jar");

    public static string ServerMappingsPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.txt");

    public static string ServerEulaPath(MCLauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string ServerPropertiesPath(MCLauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string DownloadedVersionManifestPath(MCLauncherPath launcherPath) =>
        VFS.Combine(launcherPath.Path, "version_manifest.json");

    public static string DownloadedVersionDetailsPath(MCLauncherPath launcherPath, MCVersion version) =>
        VFS.Combine(launcherPath.Path, "versions", version.ID + ".json");
}
