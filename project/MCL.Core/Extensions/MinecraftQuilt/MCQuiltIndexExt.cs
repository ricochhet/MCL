using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Extensions.MinecraftQuilt;

public static class MCQuiltIndexExt
{
    public static bool InstallerExists(this MCQuiltIndex quiltIndex)
    {
        return quiltIndex?.Installer != null && quiltIndex.Installer?.Count > 0;
    }

    public static bool LoaderExists(this MCQuiltIndex quiltIndex)
    {
        return quiltIndex?.Loader != null && quiltIndex.Loader?.Count > 0;
    }
}
