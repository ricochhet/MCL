using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class MCVersionHelperErr : IErrorHandleItem<MCVersionManifest>
{
    public static bool Exists(MCVersionManifest versionManifest)
    {
        if (versionManifest?.Versions == null)
            return false;

        if (versionManifest.Versions?.Count <= 0)
            return false;

        return true;
    }
}
