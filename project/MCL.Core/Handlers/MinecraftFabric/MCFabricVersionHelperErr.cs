using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Handlers.Minecraft;

public static class MCFabricVersionHelperErr
{
    public static bool Exists(MCFabricIndex fabricIndex)
    {
        if (fabricIndex?.Installer == null)
            return false;

        if (fabricIndex.Loader == null)
            return false;

        if (fabricIndex.Installer?.Count <= 0)
            return false;

        if (fabricIndex.Loader?.Count <= 0)
            return false;

        return true;
    }
}
