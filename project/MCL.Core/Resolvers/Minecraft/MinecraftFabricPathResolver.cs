using System.IO;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftFabricPathResolver
{
    public static string FabricLoaderPath(MCLauncherPath fabricPath)
    {
        return Path.Combine(fabricPath.FabricPath, "installers");
    }

    public static string FabricModPath(MCLauncherPath fabricPath)
    {
        return Path.Combine(fabricPath.FabricPath, "mods");
    }

    public static string DownloadedFabricLoaderPath(MCLauncherPath fabricPath, MCFabricInstaller fabricInstaller)
    {
        return Path.Combine(fabricPath.FabricPath, "installers", $"fabric-loader-{fabricInstaller.Version}.jar");
    }

    public static string DownloadedFabricIndexPath(MCLauncherPath fabricPath)
    {
        return Path.Combine(fabricPath.FabricPath, "fabric_manifest.json");
    }

    public static string FabricLoaderJarUrlPath(MCFabricConfigUrls fabricUrls, MCFabricInstaller fabricInstaller)
    {
        return fabricUrls.FabricLoaderJarUrl.Replace("{0}", fabricInstaller.Version);
    }
}
