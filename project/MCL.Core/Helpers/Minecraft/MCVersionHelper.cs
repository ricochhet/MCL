using System.Collections.Generic;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class MCVersionHelper
{
    public static MCVersion GetVersion(MCLauncherVersion launcherVersion, List<MCVersion> versions)
    {
        if (!MCLauncherVersion.Exists(launcherVersion))
            return null;

        foreach (MCVersion item in versions)
        {
            if (item.ID == launcherVersion.MCVersion)
                return item;
        }
        return null;
    }
}
