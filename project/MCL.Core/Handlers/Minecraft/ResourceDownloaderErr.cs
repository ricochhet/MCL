using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class ResourceDownloaderErr : IErrorHandleItems<MCConfigUrls, MCAssetsData>
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
