using System.IO;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftFabricPathResolver
{
    public static string FabricInstallerPath(MCLauncherPath fabricInstallerPath)
    {
        return Path.Combine(fabricInstallerPath.FabricInstallerPath, "installers");
    }

    public static string FabricModPath(MCLauncherPath fabricInstallerPath)
    {
        return Path.Combine(fabricInstallerPath.FabricInstallerPath, "mods");
    }

    public static string DownloadedFabricInstallerPath(
        MCLauncherPath fabricInstallerPath,
        MCFabricInstaller fabricInstaller
    )
    {
        return Path.Combine(
            fabricInstallerPath.FabricInstallerPath,
            "installers",
            $"fabric-installer-{fabricInstaller.Version}.jar"
        );
    }

    public static string DownloadedFabricIndexPath(MCLauncherPath fabricInstallerPath)
    {
        return Path.Combine(fabricInstallerPath.FabricInstallerPath, "fabric_manifest.json");
    }

    public static string DownloadedFabricProfilePath(
        MCLauncherPath fabricInstallerPath,
        MCLauncherVersion minecraftVersion
    )
    {
        return Path.Combine(
            fabricInstallerPath.FabricInstallerPath,
            $"fabric_profile-{minecraftVersion.MCVersion}-{minecraftVersion.FabricLoaderVersion}.json"
        );
    }

    public static string FabricLoaderJarUrlPath(MCFabricConfigUrls fabricUrls, MCLauncherVersion launcherVersion)
    {
        return fabricUrls.FabricLoaderJarUrl.Replace("{0}", launcherVersion.FabricLoaderVersion);
    }

    public static string FabricLoaderProfileUrlPath(MCFabricConfigUrls fabricUrls, MCLauncherVersion minecraftVersion)
    {
        return string.Format(
            fabricUrls.FabricLoaderProfileUrl,
            minecraftVersion.MCVersion,
            minecraftVersion.FabricLoaderVersion
        );
    }
}
