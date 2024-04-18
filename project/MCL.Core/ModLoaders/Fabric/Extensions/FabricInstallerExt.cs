using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

public static class FabricInstallerExt
{
    public static bool UrlExists(this FabricInstaller fabricInstaller)
    {
        return !string.IsNullOrWhiteSpace(fabricInstaller?.URL);
    }

    public static bool VersionExists(this FabricInstaller fabricInstaller)
    {
        return !string.IsNullOrWhiteSpace(fabricInstaller?.Version);
    }
}
