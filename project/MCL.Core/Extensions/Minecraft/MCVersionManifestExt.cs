using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MCVersionManifestExt
{
    public static bool VersionsExists(this MCVersionManifest versionManifest)
    {
        return versionManifest?.Versions != null && versionManifest.Versions?.Count > 0;
    }
}
