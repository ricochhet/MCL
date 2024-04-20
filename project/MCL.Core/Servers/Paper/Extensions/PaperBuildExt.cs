using MCL.Core.Servers.Paper.Models;

namespace MCL.Core.Servers.Paper.Extensions;

public static class PaperBuildExt
{
    public static bool BuildExists(this PaperBuild paperBuild)
    {
        return paperBuild != null
            && !string.IsNullOrWhiteSpace(paperBuild.Build.ToString())
            && !string.IsNullOrWhiteSpace(paperBuild.Downloads?.Application?.Name)
            && !string.IsNullOrWhiteSpace(paperBuild.Downloads?.Application?.SHA256);
    }
}
