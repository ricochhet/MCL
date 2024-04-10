using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Handlers.Minecraft;

public static class MCFabricConfigUrlsErr
{
    public static bool Exists(MCFabricConfigUrls fabricConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricVersionsIndex))
            return false;

        if (string.IsNullOrWhiteSpace(fabricConfigUrls.FabricLoaderProfileUrl))
            return false;

        return true;
    }
}
