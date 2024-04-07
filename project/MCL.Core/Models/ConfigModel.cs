using MCL.Core.Models.Minecraft;

namespace MCL.Core.Models;

public class ConfigModel
{
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCConfigArgs MinecraftArgs { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }
}
