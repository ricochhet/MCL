using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class VersionDetailsDownloaderErr
{
    public static bool Exists(MCVersion version)
    {
        if (string.IsNullOrWhiteSpace(version?.URL))
            return false;

        return true;
    }
}
