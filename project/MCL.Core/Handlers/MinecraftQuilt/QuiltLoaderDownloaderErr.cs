using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public static class QuiltLoaderDownloaderErr
{
    public static bool Exists(MCQuiltProfile quiltProfile, MCQuiltConfigUrls quiltConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltLoaderJarUrl))
            return false;

        if (quiltProfile?.Libraries == null)
            return false;

        return true;
    }

    public static bool Exists(MCQuiltLibrary library)
    {
        if (string.IsNullOrWhiteSpace(library.Name))
            return false;

        if (string.IsNullOrWhiteSpace(library.URL))
            return false;

        return true;
    }
}
