using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MVersionExt
{
    public static bool UrlExists(this MVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.URL);
    }

    public static bool IdExists(this MVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.ID);
    }
}
