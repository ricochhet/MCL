using MCL.Core.Models.Paper;

namespace MCL.Core.Extensions.Paper;

public static class PaperConfigUrlsExt
{
    public static bool VersionManifestExists(this PaperConfigUrls paperConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(paperConfigUrls?.PaperVersionManifest);
    }

    public static bool JarUrlExists(this PaperConfigUrls paperConfigUrls)
    {
        return string.IsNullOrWhiteSpace(paperConfigUrls?.PaperJarUrl);
    }
}
