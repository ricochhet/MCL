using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsLoggingErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Logging == null)
            return false;

        if (versionDetails.Logging.Client == null)
            return false;

        if (versionDetails.Logging.Client.File == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Logging.Client.File.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Logging.Client.File.URL))
            return false;

        return true;
    }
}
