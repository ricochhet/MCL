using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MCAssetsDataExt
{
    public static bool ObjectsExists(this MCAssetsData assets)
    {
        return assets?.Objects != null;
    }
}
