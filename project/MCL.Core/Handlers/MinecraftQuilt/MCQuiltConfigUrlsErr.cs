using MCL.Core.Interfaces;
using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Handlers.MinecraftQuilt;

public class MCQuiltConfigUrlsErr : IErrorHandleItem<MCQuiltConfigUrls>
{
    public static bool Exists(MCQuiltConfigUrls quiltConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltVersionsIndex))
            return false;

        if (string.IsNullOrWhiteSpace(quiltConfigUrls.QuiltLoaderProfileUrl))
            return false;

        return true;
    }
}
