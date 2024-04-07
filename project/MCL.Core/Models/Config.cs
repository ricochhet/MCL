using MCL.Core.Models.Minecraft;

namespace MCL.Core.Models;

public class Config
{
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }
    public MCConfigArgs MinecraftArgs { get; set; }
}
