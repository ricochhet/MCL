using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

public static class FabricVersionManifestExt
{
    public static bool InstallerExists(this FabricVersionManifest fabricVersionManifest)
    {
        return fabricVersionManifest?.Installer != null && fabricVersionManifest.Installer?.Count > 0;
    }

    public static bool LoaderExists(this FabricVersionManifest fabricVersionManifest)
    {
        return fabricVersionManifest?.Loader != null && fabricVersionManifest.Loader?.Count > 0;
    }
}
