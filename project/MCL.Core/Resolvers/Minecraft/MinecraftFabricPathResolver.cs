using System.IO;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftFabricPathResolver
{
    public static string FabricInstallerPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.FabricInstallerPath, "installers");
    }

    public static string FabricModPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.FabricInstallerPath, "mods");
    }

    public static string DownloadedFabricInstallerPath(MCLauncherPath launcherPath, MCFabricInstaller fabricInstaller)
    {
        return Path.Combine(
            launcherPath.FabricInstallerPath,
            "installers",
            $"fabric-installer-{fabricInstaller.Version}.jar"
        );
    }

    public static string DownloadedFabricIndexPath(MCLauncherPath launcherPath)
    {
        return Path.Combine(launcherPath.FabricInstallerPath, "fabric_manifest.json");
    }

    public static string DownloadedFabricProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion)
    {
        return Path.Combine(
            launcherPath.FabricInstallerPath,
            $"fabric_profile-{launcherVersion.MCVersion}-{launcherVersion.FabricLoaderVersion}.json"
        );
    }

    public static string FabricLoaderJarUrlPath(MCFabricConfigUrls fabricConfigUrls, MCLauncherVersion launcherVersion)
    {
        return fabricConfigUrls.FabricLoaderJarUrl.Replace("{0}", launcherVersion.FabricLoaderVersion);
    }

    public static string FabricLoaderProfileUrlPath(
        MCFabricConfigUrls fabricConfigUrls,
        MCLauncherVersion launcherVersion
    )
    {
        return string.Format(
            fabricConfigUrls.FabricLoaderProfileUrl,
            launcherVersion.MCVersion,
            launcherVersion.FabricLoaderVersion
        );
    }
}
