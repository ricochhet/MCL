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

    public static string VersionPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(minecraftPath, "versions", versionDetails.ID);
    }

    public static string ClientJarPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), versionDetails.ID + ".jar");
    }

    public static string ClientMappingsPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.txt");
    }

    public static string ClientIndexPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(AssetsPath(minecraftPath), "indexes", versionDetails.Assets + ".json");
    }

    public static string LoggingPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.xml");
    }

    public static string ServerJarPath(string minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(minecraftPath), $"minecraft_server.{versionDetails.ID}.jar");
    }

    public static string ServerMappingsPath(string minecraftPath, MCVersionDetails versionDetails)
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

    public static string DownloadedVersionDetailsPath(string minecraftPath, MCVersion version)
    {
        return Path.Combine(minecraftPath, "versions", version.ID + ".json");
    }

    public static string DownloadedJavaRuntimeIndexPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "java_runtime_index.json");
    }

    public static string DownloadedJavaRuntimeManifestPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "java_runtime_manifest.json");
    }

    public static string DownloadedJavaRuntimePath(string minecraftPath, string javaRuntimeVersion)
    {
        return Path.Combine(minecraftPath, "runtime", javaRuntimeVersion);
    }
}
