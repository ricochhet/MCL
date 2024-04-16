using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Extensions.MinecraftQuilt;

public static class MCQuiltConfigUrlsExt
{
    public static bool VersionsIndexExists(this MCQuiltConfigUrls quiltConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltVersionsIndex);
    }

    public static bool LoaderProfileUrlExists(this MCQuiltConfigUrls quiltConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltLoaderProfileUrl);
    }

    public static bool LoaderJarUrlExists(this MCQuiltConfigUrls quiltConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltLoaderJarUrl);
    }

    public static bool ApiLoaderNameExists(this MCQuiltConfigUrls quiltConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this MCQuiltConfigUrls quiltConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(quiltConfigUrls?.QuiltApiIntermediaryName);
    }
}
