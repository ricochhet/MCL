using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class LoggingDownloaderErr : IErrorHandleItem<MCVersionDetails>
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Logging?.Client?.File?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Logging?.Client?.File?.URL))
            return false;

        return true;
    }
}
