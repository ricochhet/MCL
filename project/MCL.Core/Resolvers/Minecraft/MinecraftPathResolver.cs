using System.Collections.Generic;
using System.IO;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftPathResolver
{
    public static string ClientLibrary(string minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion, $"{minecraftVersion}.jar").Replace("\\", "/");
    }

    public static string Libraries(string minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion, $"{minecraftVersion}-natives").Replace("\\", "/");
    }

    public static int AssetIndexId(string minecraftPath)
    {
        List<string> fileName = FsProvider.GetFiles(Path.Combine(AssetsPath(minecraftPath), "indexes"), "*.json", true);
        bool success = int.TryParse(fileName[0], out int id);
        if (success)
            return id;
        return -1;
    }

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

    public static string JavaRuntimePath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "runtime");
    }

    public static string DownloadedJavaRuntimeIndexPath(string minecraftPath)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), "java_runtime_index.json");
    }

    public static string DownloadedJavaRuntimeManifestPath(string minecraftPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), javaRuntimeVersion, "java_runtime_manifest.json");
    }

    public static string DownloadedJavaRuntimePath(string minecraftPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), javaRuntimeVersion);
    }
}
