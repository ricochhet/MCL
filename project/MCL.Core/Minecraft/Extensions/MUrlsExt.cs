using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MUrlsExt
{
    public static bool JavaVersionManifestExists(this MUrls mUrls)
    {
        return !string.IsNullOrWhiteSpace(mUrls?.JavaVersionManifest);
    }

    public static bool MinecraftResourcesExists(this MUrls mUrls)
    {
        return !string.IsNullOrWhiteSpace(mUrls?.MinecraftResources);
    }

    public static bool VersionManifestExists(this MUrls mUrls)
    {
        return !string.IsNullOrWhiteSpace(mUrls?.VersionManifest);
    }
}
