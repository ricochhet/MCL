using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCConfigVersionManifestErr
{
    public static bool Exists(MCConfigUrls configUrls)
    {
        if (configUrls == null)
            return false;

        if (string.IsNullOrWhiteSpace(configUrls.VersionManifest))
            return false;

        return true;
    }
}
