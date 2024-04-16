using MCL.Core.Models.Paper;

namespace MCL.Core.Extensions.Paper;

public static class PaperVersionManifestExt
{
    public static bool BuildsExists(this PaperVersionManifest paperVersionManifest)
    {
        return paperVersionManifest?.Builds != null && paperVersionManifest.Builds?.Count > 0;
    }
}
