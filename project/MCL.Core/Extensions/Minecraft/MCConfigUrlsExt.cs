using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MCConfigUrlsExt
{
    public static bool JavaRuntimeIndexUrlExists(this MCConfigUrls configUrls)
    {
        return !string.IsNullOrWhiteSpace(configUrls?.JavaRuntimeIndexUrl);
    }

    public static bool MinecraftResourcesExists(this MCConfigUrls configUrls)
    {
        return !string.IsNullOrWhiteSpace(configUrls?.MinecraftResources);
    }

    public static bool VersionManifestExists(this MCConfigUrls configUrls)
    {
        return !string.IsNullOrWhiteSpace(configUrls?.VersionManifest);
    }
}
