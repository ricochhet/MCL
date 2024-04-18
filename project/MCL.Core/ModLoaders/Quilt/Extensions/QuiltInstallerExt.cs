using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltInstallerExt
{
    public static bool UrlExists(this QuiltInstaller quiltInstaller)
    {
        return !string.IsNullOrWhiteSpace(quiltInstaller?.URL);
    }

    public static bool VersionExists(this QuiltInstaller quiltInstaller)
    {
        return !string.IsNullOrWhiteSpace(quiltInstaller?.Version);
    }
}
