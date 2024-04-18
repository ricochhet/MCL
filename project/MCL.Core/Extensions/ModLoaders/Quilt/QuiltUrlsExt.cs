using MCL.Core.Models.ModLoaders.Quilt;

namespace MCL.Core.Extensions.ModLoaders.Quilt;

public static class QuiltUrlsExt
{
    public static bool VersionsIndexExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.QuiltVersionsIndex);
    }

    public static bool LoaderProfileUrlExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.QuiltLoaderProfileUrl);
    }

    public static bool LoaderJarUrlExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.QuiltLoaderJarUrl);
    }

    public static bool ApiLoaderNameExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.QuiltApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this QuiltUrls quiltUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltUrls?.QuiltApiIntermediaryName);
    }
}
