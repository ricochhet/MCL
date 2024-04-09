using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsClientErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.Downloads == null)
            return false;

        if (versionDetails.Downloads.Client == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Client.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads.Client.URL))
            return false;

        return true;
    }
}
