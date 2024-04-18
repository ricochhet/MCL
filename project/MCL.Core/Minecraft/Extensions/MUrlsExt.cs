using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MUrlsExt
{
    public static bool JavaRuntimeIndexUrlExists(this MUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.JavaRuntimeIndexUrl);
    }

    public static bool MinecraftResourcesExists(this MUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.MinecraftResources);
    }

    public static bool VersionManifestExists(this MUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.VersionManifest);
    }
}
