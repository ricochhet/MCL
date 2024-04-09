using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Java;

public static class JavaRuntimeIndexUrlErr
{
    public static bool Exists(MCConfigUrls configUrls)
    {
        if (configUrls == null)
            return false;

        if (string.IsNullOrWhiteSpace(configUrls.JavaRuntimeIndexUrl))
            return false;

        return true;
    }
}
