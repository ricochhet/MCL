using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class ServerDownloaderErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Server?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads?.Server?.URL))
            return false;

        return true;
    }
}
