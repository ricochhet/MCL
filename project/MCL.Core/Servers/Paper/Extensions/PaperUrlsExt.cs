using MCL.Core.Servers.Paper.Models;

namespace MCL.Core.Servers.Paper.Extensions;

public static class PaperUrlsExt
{
    public static bool VersionManifestExists(this PaperUrls paperUrls)
    {
        return !string.IsNullOrWhiteSpace(paperUrls?.VersionManifest);
    }

    public static bool JarUrlExists(this PaperUrls paperUrls)
    {
        return !string.IsNullOrWhiteSpace(paperUrls?.PaperJar);
    }
}
