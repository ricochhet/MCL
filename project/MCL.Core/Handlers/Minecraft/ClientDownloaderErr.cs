using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class ClientDownloaderErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Client?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Client?.URL))
            return false;

        return true;
    }
}
