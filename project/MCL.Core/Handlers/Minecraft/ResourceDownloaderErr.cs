using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class ResourceDownloaderErr
{
    public static bool Exists(MCConfigUrls configUrls, MCAssetsData assets)
    {
        if (assets?.Objects == null)
            return false;

        if (string.IsNullOrWhiteSpace(configUrls?.MinecraftResources))
            return false;

        return true;
    }
}
