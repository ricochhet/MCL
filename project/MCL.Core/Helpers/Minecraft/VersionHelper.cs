using System;
using System.Collections.Generic;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static Models.Minecraft.Version GetVersion(string minecraftVersion, List<Models.Minecraft.Version> versions)
    {
        foreach (Models.Minecraft.Version item in versions)
        {
            if (item.ID == minecraftVersion)
                return item;
        }
        throw new Exception($"Can't find version {minecraftVersion}");
    }
}
