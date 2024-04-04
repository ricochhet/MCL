using MCL.Core.Models;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Config.Minecraft;

public class MinecraftUrlConfig
{
    public readonly MinecraftUrls URL;

    public MinecraftUrlConfig()
    {
        URL = new()
        {
            VersionManifest = "https://launchermeta.mojang.com/mc/game/version_manifest.json",
            MinecraftResources = "https://resources.download.minecraft.net",
        };
    }
}
