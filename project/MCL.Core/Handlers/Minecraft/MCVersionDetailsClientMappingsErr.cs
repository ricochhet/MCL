using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsClientMappingsErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.ClientMappings == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.ClientMappings.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.ClientMappings.URL))
            return false;

        return true;
    }
}
