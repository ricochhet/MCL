using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MCVersionExt
{
    public static bool UrlExists(this MCVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.URL);
    }

    public static bool IdExists(this MCVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.ID);
    }
}
