using MCL.Core.Launcher.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.MiniCommon;

namespace MCL.Core.Minecraft.Resolvers;

public static class MPathResolver
{
    public static string ClientLibrary(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}.jar").Replace("\\", "/");

    public static string Libraries(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, $"{launcherVersion.Version}-natives").Replace("\\", "/");

    public static string AssetsPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "assets");

    public static string LibraryPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "libraries");

    public static string ServerPath(LauncherPath launcherPath) => VFS.Combine(launcherPath.Path, "server");

    public static string VersionPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(launcherPath.Path, "versions", versionDetails.ID);

    public static string ClientJarPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), versionDetails.ID + ".jar");

    public static string ClientMappingsPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.txt");

    public static string ClientIndexPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(AssetsPath(launcherPath), "indexes", versionDetails.Assets + ".json");

    public static string LoggingPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(VersionPath(launcherPath, versionDetails), "client.xml");

    public static string LoggingPath(LauncherVersion launcherVersion) =>
        VFS.Combine("versions", launcherVersion.Version, "client.xml");

    public static string ServerJarPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.jar");

    public static string ServerMappingsPath(LauncherPath launcherPath, MVersionDetails versionDetails) =>
        VFS.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.txt");

    public static string ServerEulaPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string ServerPropertiesPath(LauncherPath launcherPath) =>
        VFS.Combine(ServerPath(launcherPath), "server.properties");

    public static string DownloadedVersionManifestPath(LauncherPath launcherPath) =>
        VFS.Combine(launcherPath.Path, "version_manifest.json");

    public static string DownloadedVersionDetailsPath(LauncherPath launcherPath, MVersion version) =>
        VFS.Combine(launcherPath.Path, "versions", version.ID + ".json");
}
