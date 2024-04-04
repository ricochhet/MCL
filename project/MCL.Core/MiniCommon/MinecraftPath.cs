using System.IO;
using MCL.Core.Models;

namespace MCL.Core.MiniCommon;

public static class MinecraftPath
{
    public static string AssetsPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "assets");
    }

    public static string ClientPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(minecraftPath, "versions", versionDetails.ID, versionDetails.ID + ".jar");
    }

    public static string ServerPath(string minecraftPath, VersionDetails versionDetails)
    {
        return Path.Combine(minecraftPath, "server", $"minecraft_server.{versionDetails.ID}.jar");
    }

    public static string ServerEulaPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "server", "server.properties");
    }

    public static string ServerPropertiesPath(string minecraftPath)
    {
        return Path.Combine(minecraftPath, "server", "server.properties");
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
