using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Extensions.ModLoaders.Fabric;

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
