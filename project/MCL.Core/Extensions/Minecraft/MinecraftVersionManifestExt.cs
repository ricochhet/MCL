using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MinecraftVersionManifestExt
{
    public static bool VersionsExists(this MinecraftVersionManifest versionManifest)
    {
        return versionManifest?.Versions != null && versionManifest.Versions?.Count > 0;
    }
}
