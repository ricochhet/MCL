using MCL.Core.Servers.Paper.Models;

namespace MCL.Core.Servers.Paper.Extensions;

public static class PaperVersionManifestExt
{
    public static bool BuildsExists(this PaperVersionManifest paperVersionManifest)
    {
        return paperVersionManifest?.Builds != null && paperVersionManifest.Builds?.Count > 0;
    }
}
