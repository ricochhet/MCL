using MCL.Core.Models.ModLoaders.Quilt;

namespace MCL.Core.Extensions.ModLoaders.Quilt;

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
