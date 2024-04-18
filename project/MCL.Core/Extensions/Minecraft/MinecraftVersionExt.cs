using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MinecraftVersionExt
{
    public static bool UrlExists(this MinecraftVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.URL);
    }

    public static bool IdExists(this MinecraftVersion version)
    {
        return !string.IsNullOrWhiteSpace(version?.ID);
    }
}
