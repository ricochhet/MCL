using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MAssetsDataExt
{
    public static bool ObjectsExists(this MAssetsData assets)
    {
        return assets?.Objects != null;
    }
}
