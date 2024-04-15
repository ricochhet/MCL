using MCL.Core.Interfaces;
using MCL.Core.Models.Paper;

namespace MCL.Core.Handlers.Paper;

public class PaperConfigUrlsErr : IErrorHandleItem<PaperConfigUrls>
{
    public static bool Exists(PaperConfigUrls paperConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(paperConfigUrls?.PaperVersionManifest))
            return false;

        if (string.IsNullOrWhiteSpace(paperConfigUrls.PaperJarUrl))
            return false;

        return true;
    }
}
