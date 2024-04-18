using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltIndexExt
{
    public static bool InstallerExists(this QuiltIndex quiltIndex)
    {
        return quiltIndex?.Installer != null && quiltIndex.Installer?.Count > 0;
    }

    public static bool LoaderExists(this QuiltIndex quiltIndex)
    {
        return quiltIndex?.Loader != null && quiltIndex.Loader?.Count > 0;
    }
}
