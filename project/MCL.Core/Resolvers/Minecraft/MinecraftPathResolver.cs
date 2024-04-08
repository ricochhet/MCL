using System.IO;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftPathResolver
{
    public static string ClientLibrary(MCLauncherVersion launcherVersion)
    {
        return Path.Combine("versions", launcherVersion.MCVersion, $"{launcherVersion.MCVersion}.jar")
            .Replace("\\", "/");
    }

    public static string Libraries(MCLauncherVersion launcherVersion)
    {
        return Path.Combine("versions", launcherVersion.MCVersion, $"{launcherVersion.MCVersion}-natives")
            .Replace("\\", "/");
    }

    public static int AssetIndexId(MCLauncherPath launcherPath)
    {
        string[] fileName = VFS.GetFiles(
            Path.Combine(AssetsPath(launcherPath), "indexes"),
            "*.json",
            SearchOption.AllDirectories,
            false
        );
        bool success = int.TryParse(fileName[0], out int id);
        if (success)
            return id;
        return -1;
    }

    public static string AssetsPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.MCPath, "assets");
    }

    public static string LibraryPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.MCPath, "libraries");
    }

    public static string ServerPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.MCPath, "server");
    }

    public static string VersionPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(launcherPath.MCPath, "versions", versionDetails.ID);
    }

    public static string ClientJarPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(launcherPath, versionDetails), versionDetails.ID + ".jar");
    }

    public static string ClientMappingsPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(launcherPath, versionDetails), "client.txt");
    }

    public static string ClientIndexPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(AssetsPath(launcherPath), "indexes", versionDetails.Assets + ".json");
    }

    public static string LoggingPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(launcherPath, versionDetails), "client.xml");
    }

    public static string ServerJarPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.jar");
    }

    public static string ServerMappingsPath(MCLauncherPath launcherPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(launcherPath), $"minecraft_server.{versionDetails.ID}.txt");
    }

    public static string ServerEulaPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(ServerPath(launcherPath), "server.properties");
    }

    public static string ServerPropertiesPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(ServerPath(launcherPath), "server.properties");
    }

    public static string DownloadedVersionManifestPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.MCPath, "version_manifest.json");
    }

    public static string DownloadedVersionDetailsPath(MCLauncherPath launcherPath, MCVersion version)
    {
        return Path.Combine(launcherPath.MCPath, "versions", version.ID + ".json");
    }
}
