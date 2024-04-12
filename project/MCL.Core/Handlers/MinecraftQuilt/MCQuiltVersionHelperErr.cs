using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Handlers.MinecraftQuilt;

public static class MCQuiltVersionHelperErr
{
    public static bool Exists(MCQuiltIndex quiltIndex)
    {
        if (quiltIndex?.Installer == null)
            return false;

        if (quiltIndex.Loader == null)
            return false;

        if (quiltIndex.Installer?.Count <= 0)
            return false;

        if (quiltIndex.Loader?.Count <= 0)
            return false;

        return true;
    }
}
