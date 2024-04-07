using System.Collections.Generic;
using System.IO;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Providers;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftPathResolver
{
    public static string ClientLibrary(MCLauncherVersion minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion.MCVersion, $"{minecraftVersion}.jar").Replace("\\", "/");
    }

    public static string Libraries(MCLauncherVersion minecraftVersion)
    {
        return Path.Combine("versions", minecraftVersion.MCVersion, $"{minecraftVersion}-natives").Replace("\\", "/");
    }

    public static int AssetIndexId(MCLauncherPath minecraftPath)
    {
        List<string> fileName = FsProvider.GetFiles(Path.Combine(AssetsPath(minecraftPath), "indexes"), "*.json", true);
        bool success = int.TryParse(fileName[0], out int id);
        if (success)
            return id;
        return -1;
    }

    public static string AssetsPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(minecraftPath.MCPath, "assets");
    }

    public static string LibraryPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(minecraftPath.MCPath, "libraries");
    }

    public static string ServerPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(minecraftPath.MCPath, "server");
    }

    public static string VersionPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(minecraftPath.MCPath, "versions", versionDetails.ID);
    }

    public static string ClientJarPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), versionDetails.ID + ".jar");
    }

    public static string ClientMappingsPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.txt");
    }

    public static string ClientIndexPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(AssetsPath(minecraftPath), "indexes", versionDetails.Assets + ".json");
    }

    public static string LoggingPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(VersionPath(minecraftPath, versionDetails), "client.xml");
    }

    public static string ServerJarPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(minecraftPath), $"minecraft_server.{versionDetails.ID}.jar");
    }

    public static string ServerMappingsPath(MCLauncherPath minecraftPath, MCVersionDetails versionDetails)
    {
        return Path.Combine(ServerPath(minecraftPath), $"minecraft_server.{versionDetails.ID}.txt");
    }

    public static string ServerEulaPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(ServerPath(minecraftPath), "server.properties");
    }

    public static string ServerPropertiesPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(ServerPath(minecraftPath), "server.properties");
    }

    public static string DownloadedVersionManifestPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(minecraftPath.MCPath, "version_manifest.json");
    }

    public static string DownloadedVersionDetailsPath(MCLauncherPath minecraftPath, MCVersion version)
    {
        return Path.Combine(minecraftPath.MCPath, "versions", version.ID + ".json");
    }

    public static string JavaRuntimePath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(minecraftPath.MCPath, "runtime");
    }

    public static string DownloadedJavaRuntimeIndexPath(MCLauncherPath minecraftPath)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), "java_runtime_index.json");
    }

    public static string DownloadedJavaRuntimeManifestPath(MCLauncherPath minecraftPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), javaRuntimeVersion, "java_runtime_manifest.json");
    }

    public static string DownloadedJavaRuntimePath(MCLauncherPath minecraftPath, string javaRuntimeVersion)
    {
        return Path.Combine(JavaRuntimePath(minecraftPath), javaRuntimeVersion);
    }
}
