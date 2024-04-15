using MCL.Core.Interfaces;
using MCL.Core.Models.Paper;

namespace MCL.Core.Handlers.Paper;

public class PaperServerDownloaderErr : IErrorHandleItem<PaperVersionManifest>
{
    public static bool Exists(PaperVersionManifest paperVersionManifest)
    {
        if (paperVersionManifest?.Builds == null || paperVersionManifest.Builds?.Count <= 0)
            return false;

        PaperBuild paperBuild = paperVersionManifest.Builds[^1];
        if (paperBuild == null)
            return false;

        if (string.IsNullOrWhiteSpace(paperBuild.Build.ToString()))
            return false;

        if (string.IsNullOrWhiteSpace(paperBuild.Downloads?.Application?.Name))
            return false;

        return true;
    }
}
