using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class LoggingDownloaderErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Logging?.Client?.File?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails?.Logging?.Client?.File?.URL))
            return false;

        return true;
    }
}
