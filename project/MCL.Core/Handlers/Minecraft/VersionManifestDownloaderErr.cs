using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class VersionManifestDownloaderErr : IErrorHandleItem<MCConfigUrls>
{
    public static bool Exists(MCConfigUrls configUrls)
    {
        if (string.IsNullOrWhiteSpace(configUrls?.VersionManifest))
            return false;

        return true;
    }
}
