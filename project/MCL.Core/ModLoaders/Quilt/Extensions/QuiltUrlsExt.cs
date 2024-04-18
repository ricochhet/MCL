using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltUrlsExt
{
    public static bool VersionManifestExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.VersionManifest);
    }

    public static bool LoaderProfileExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.LoaderProfile);
    }

    public static bool LoaderJarExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.LoaderJar);
    }

    public static bool ApiLoaderNameExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.ApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.ApiIntermediaryName);
    }
}
