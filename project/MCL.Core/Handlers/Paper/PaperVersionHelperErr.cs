using MCL.Core.Interfaces;
using MCL.Core.Models.Paper;

namespace MCL.Core.Handlers.MinecraftFabric;

public class PaperVersionHelperErr : IErrorHandleItem<PaperVersionManifest>
{
    public static bool Exists(PaperVersionManifest paperVersionManifest)
    {
        if (paperVersionManifest?.Builds == null)
            return false;

        if (paperVersionManifest.Builds?.Count <= 0)
            return false;

        return true;
    }
}
