using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MinecraftUrlsExt
{
    public static bool JavaRuntimeIndexUrlExists(this MinecraftUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.JavaRuntimeIndexUrl);
    }

    public static bool MinecraftResourcesExists(this MinecraftUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.MinecraftResources);
    }

    public static bool VersionManifestExists(this MinecraftUrls minecraftUrls)
    {
        return !string.IsNullOrWhiteSpace(minecraftUrls?.VersionManifest);
    }
}
