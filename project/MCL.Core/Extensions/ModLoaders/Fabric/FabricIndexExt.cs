using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Extensions.ModLoaders.Fabric;

public static class FabricIndexExt
{
    public static bool InstallerExists(this FabricIndex fabricIndex)
    {
        return fabricIndex?.Installer != null && fabricIndex.Installer?.Count > 0;
    }

    public static bool LoaderExists(this FabricIndex fabricIndex)
    {
        return fabricIndex?.Loader != null && fabricIndex.Loader?.Count > 0;
    }
}
