using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsServerErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.Server == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Server.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Server.URL))
            return false;

        return true;
    }
}
