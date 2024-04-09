using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class VersionManifestDownloaderErr
{
    public static bool Exists(MCConfigUrls configUrls)
    {
        if (string.IsNullOrWhiteSpace(configUrls?.VersionManifest))
            return false;

        return true;
    }
}
