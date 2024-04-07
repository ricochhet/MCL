using System;
using System.Collections.Generic;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static MCVersion GetVersion(string minecraftVersion, List<MCVersion> versions)
    {
        foreach (MCVersion item in versions)
        {
            if (item.ID == minecraftVersion)
                return item;
        }
        throw new Exception($"Can't find version {minecraftVersion}");
    }

    public static MCFabricInstaller GetFabricVersion(string fabricVersion, List<MCFabricInstaller> installers)
    {
        foreach (MCFabricInstaller item in installers)
        {
            if (item.Version == fabricVersion)
                return item;
        }
        throw new Exception($"Can't find version {fabricVersion}");
    }
}
