using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Extensions.MinecraftFabric;

public static class MCFabricInstallerExt
{
    public static bool UrlExists(this MCFabricInstaller fabricInstaller)
    {
        return !string.IsNullOrWhiteSpace(fabricInstaller?.URL);
    }

    public static bool VersionExists(this MCFabricInstaller fabricInstaller)
    {
        return !string.IsNullOrWhiteSpace(fabricInstaller?.Version);
    }
}
