using System;
using System.Collections.Generic;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static Models.Minecraft.MCVersion GetVersion(
        string minecraftVersion,
        List<Models.Minecraft.MCVersion> versions
    )
    {
        foreach (Models.Minecraft.MCVersion item in versions)
        {
            if (item.ID == minecraftVersion)
                return item;
        }
        throw new Exception($"Can't find version {minecraftVersion}");
    }
}
