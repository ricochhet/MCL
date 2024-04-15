using MCL.Core.Interfaces;
using MCL.Core.Models.Paper;

namespace MCL.Core.Handlers.Paper;

public class PaperServerDownloaderErr : IErrorHandleItem<PaperBuild>
{
    public static bool Exists(PaperBuild paperBuild)
    {
        if (paperBuild == null)
            return false;

        if (string.IsNullOrWhiteSpace(paperBuild.Build.ToString()))
            return false;

        if (string.IsNullOrWhiteSpace(paperBuild.Downloads?.Application?.Name))
            return false;

        return true;
    }
}
