using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Models;

public class Config
{
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }

    public MCConfigArgs MinecraftArgs { get; set; }
    public MCFabricConfigArgs FabricArgs { get; set; }
}
