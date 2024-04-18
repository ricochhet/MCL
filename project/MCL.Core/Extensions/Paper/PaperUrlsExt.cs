using MCL.Core.Models.Paper;

namespace MCL.Core.Extensions.Paper;

public static class PaperUrlsExt
{
    public static bool VersionManifestExists(this PaperUrls paperUrls)
    {
        return !string.IsNullOrWhiteSpace(paperUrls?.PaperVersionManifest);
    }

    public static bool JarUrlExists(this PaperUrls paperUrls)
    {
        return !string.IsNullOrWhiteSpace(paperUrls?.PaperJarUrl);
    }
}
