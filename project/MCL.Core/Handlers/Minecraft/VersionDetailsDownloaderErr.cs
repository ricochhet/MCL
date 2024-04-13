using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class VersionDetailsDownloaderErr : IErrorHandleItem<MCVersion>
{
    public static bool Exists(MCVersion version)
    {
        if (string.IsNullOrWhiteSpace(version?.URL))
            return false;

        return true;
    }
}
