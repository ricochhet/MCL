using MCL.Core.Models.ModLoaders.Quilt;

namespace MCL.Core.Extensions.ModLoaders.Quilt;

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
