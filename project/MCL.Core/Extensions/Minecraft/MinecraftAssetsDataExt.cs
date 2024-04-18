using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MinecraftAssetsDataExt
{
    public static bool ObjectsExists(this MinecraftAssetsData assets)
    {
        return assets?.Objects != null;
    }
}
