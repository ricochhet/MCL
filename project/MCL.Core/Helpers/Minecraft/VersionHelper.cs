using System;
using System.Collections.Generic;
using MCL.Core.Models.Minecraft;

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
}
