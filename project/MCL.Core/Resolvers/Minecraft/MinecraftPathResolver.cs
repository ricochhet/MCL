using System.IO;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftPathResolver
{
    public static string AssetsPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "assets");
    }

    public static string LibraryPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "libraries");
    }

    public static string ServerPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "server");
    }

    public static string VersionPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(minecraftPath, "versions", versionDetails.ID);
    }

    public static string ClientJarPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), versionDetails.ID + ".jar");
    }

    public static string ClientMappingsPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.txt");
    }

    public static string ClientIndexPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(AssetsPath(minecraftPath), "indexes", versionDetails.Assets + ".json");
    }

    public static string LoggingPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.xml");
    }

    public static string ServerJarPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(minecraftPath), $"minecraft_server.{versionDetails.ID}.jar");
    }

    public static string ServerMappingsPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(minecraftPath), $"minecraft_server.{versionDetails.ID}.txt");
    }

    public static string ServerEulaPath(string minecraftPath)
    {
        return Path.Combine(ServerPath(minecraftPath), "server.properties");
    }

    public static string ServerPropertiesPath(string minecraftPath)
    {
        return Path.Combine(ServerPath(minecraftPath), "server.properties");
    }

    public static string DownloadedVersionManifestPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "version_manifest.json");
    }

    public static string DownloadedVersionDetailsPath(string minecraftPath, Version version)
    {
        return Path.Combine(minecraftPath, "versions", version.ID + ".json");
    }
}