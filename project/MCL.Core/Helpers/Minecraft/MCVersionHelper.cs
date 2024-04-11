using MCL.Core.Handlers.Minecraft;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class MCVersionHelper
{
    public static MCVersion GetVersion(MCLauncherVersion launcherVersion, MCVersionManifest versionManifest)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return null;

        if (!MCVersionHelperErr.Exists(versionManifest))
            return null;

        foreach (MCVersion item in versionManifest.Versions)
        {
            if (item.ID == launcherVersion.Version)
                return item;
        }
        return null;
    }
}
