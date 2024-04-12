using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Handlers.MinecraftQuilt;

public static class MCQuiltConfigUrlsErr
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
