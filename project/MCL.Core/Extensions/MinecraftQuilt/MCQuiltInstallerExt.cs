using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Extensions.MinecraftQuilt;

public static class MCQuiltInstallerExt
{
    public static bool UrlExists(this MCQuiltInstaller quiltInstaller)
    {
        return !string.IsNullOrWhiteSpace(quiltInstaller?.URL);
    }

    public static bool VersionExists(this MCQuiltInstaller quiltInstaller)
    {
        return !string.IsNullOrWhiteSpace(quiltInstaller?.Version);
    }
}
