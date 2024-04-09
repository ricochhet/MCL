using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCAssetsDataErr
{
    public static bool Exists(MCConfigUrls configUrls, MCAssetsData assets)
    {
        if (assets == null)
            return false;

        if (assets.Objects == null)
            return false;

        if (configUrls == null)
            return false;

        if (string.IsNullOrWhiteSpace(configUrls.MinecraftResources))
            return false;

        return true;
    }
}
