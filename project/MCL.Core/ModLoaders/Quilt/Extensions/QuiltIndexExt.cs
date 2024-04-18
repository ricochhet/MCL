using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltVersionManifestExt
{
    public static bool InstallerExists(this QuiltVersionManifest quiltVersionManifest)
    {
        return quiltVersionManifest?.Installer != null && quiltVersionManifest.Installer?.Count > 0;
    }

    public static bool LoaderExists(this QuiltVersionManifest quiltVersionManifest)
    {
        return quiltVersionManifest?.Loader != null && quiltVersionManifest.Loader?.Count > 0;
    }
}
