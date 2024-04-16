using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Extensions.MinecraftFabric;

public static class MCFabricIndexExt
{
    public static bool InstallerExists(this MCFabricIndex fabricIndex)
    {
        return fabricIndex?.Installer != null && fabricIndex.Installer?.Count > 0;
    }

    public static bool LoaderExists(this MCFabricIndex fabricIndex)
    {
        return fabricIndex?.Loader != null && fabricIndex.Loader?.Count > 0;
    }
}
