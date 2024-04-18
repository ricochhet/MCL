using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MVersionManifestExt
{
    public static bool VersionsExists(this MVersionManifest versionManifest)
    {
        return versionManifest?.Versions != null && versionManifest.Versions?.Count > 0;
    }
}
