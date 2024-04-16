using MCL.Core.Models.Paper;

namespace MCL.Core.Extensions.Paper;

public static class PaperBuildExt
{
    public static bool BuildExists(this PaperBuild paperBuild)
    {
        return paperBuild != null
            && !string.IsNullOrWhiteSpace(paperBuild.Build.ToString())
            && !string.IsNullOrWhiteSpace(paperBuild.Downloads?.Application?.Name);
    }
}
