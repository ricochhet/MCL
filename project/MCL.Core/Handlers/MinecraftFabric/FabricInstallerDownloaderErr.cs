using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Web.MinecraftFabric;

public static class FabricInstallerDownloaderErr
{
    public static bool Exists(MCFabricInstaller fabricInstaller)
    {
        if (string.IsNullOrWhiteSpace(fabricInstaller?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(fabricInstaller.Version))
            return false;

        return true;
    }
}
