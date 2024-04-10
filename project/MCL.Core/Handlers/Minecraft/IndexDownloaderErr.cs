using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class IndexDownloaderErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.AssetIndex?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails?.AssetIndex?.URL))
            return false;

        return true;
    }
}
