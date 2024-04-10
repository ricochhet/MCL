using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Web.Minecraft;

public class FabricLoaderDownloaderErr
{
    public static bool Exists(MCFabricProfile fabricProfile, MCFabricConfigUrls fabricConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricLoaderJarUrl))
            return false;

        if (fabricProfile?.Libraries == null)
            return false;

        return true;
    }
}
