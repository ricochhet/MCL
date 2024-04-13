using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Java;

public class JavaRuntimeIndexDownloaderErr : IErrorHandleItem<MCConfigUrls>
{
    public static bool Exists(MCConfigUrls configUrls)
    {
        if (string.IsNullOrWhiteSpace(configUrls?.JavaRuntimeIndexUrl))
            return false;

        return true;
    }
}
