using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsServerMappingsErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.ServerMappings == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.ServerMappings.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.ServerMappings.URL))
            return false;

        return true;
    }
}
