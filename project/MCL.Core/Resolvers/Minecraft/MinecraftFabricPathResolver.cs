using System.IO;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Resolvers.Minecraft;

public static class MinecraftFabricPathResolver
{
    public static string FabricLoaderPath(string fabricPath)
    {
        return Path.Combine(fabricPath, "installers");
    }

    public static string FabricModPath(string fabricPath)
    {
        return Path.Combine(fabricPath, "mods");
    }

    public static string DownloadedFabricLoaderPath(string fabricPath, MCFabricInstaller fabricInstaller)
    {
        return Path.Combine(fabricPath, "installers", $"fabric-loader-{fabricInstaller.Version}.jar");
    }

    public static string DownloadedFabricIndexPath(string fabricPath)
    {
        return Path.Combine(fabricPath, "fabric_manifest.json");
    }

    public static string FabricLoaderJarUrlPath(MCFabricConfigUrls fabricUrls, MCFabricInstaller fabricInstaller)
    {
        return fabricUrls.FabricLoaderJarUrl.Replace("{0}", fabricInstaller.Version);
    }
}
