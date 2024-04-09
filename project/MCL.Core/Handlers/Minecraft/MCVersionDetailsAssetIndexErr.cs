using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class MCVersionDetailsAssetIndexErr
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (versionDetails == null)
            return false;

        if (versionDetails.AssetIndex == null)
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.AssetIndex.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.AssetIndex.URL))
            return false;

        return true;
    }
}
