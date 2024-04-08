using System.Collections.Generic;
using MCL.Core.Enums;
using MCL.Core.Enums.Java;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

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
