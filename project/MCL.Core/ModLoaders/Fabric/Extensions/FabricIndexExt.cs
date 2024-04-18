using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

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
