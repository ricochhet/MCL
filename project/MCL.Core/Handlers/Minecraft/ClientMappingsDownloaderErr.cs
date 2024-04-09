using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class ClientMappingsDownloaderErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ClientMappings?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ClientMappings?.URL))
            return false;

        return true;
    }
}
