using System;
using System.Collections.Generic;

namespace MCL.Core.MiniCommon;

public static class VersionHelper
{
    public static Models.Version GetVersion(string minecraftVersion, List<Models.Version> versions)
    {
        foreach (Models.Version item in versions)
        {
            if (item.ID == minecraftVersion)
                return item;
        }
        throw new Exception($"Can't find version {minecraftVersion}");
    }
}
