using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Models;

public class Config
{
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }
    public JvmArguments MinecraftArgs { get; set; }
    public JvmArguments FabricArgs { get; set; }

    public Config() { }

    public Config(
        MCConfigUrls minecraftUrls,
        MCFabricConfigUrls fabricUrls,
        JvmArguments minecraftArgs,
        JvmArguments fabricArgs
    )
    {
        MinecraftUrls = minecraftUrls;
        FabricUrls = fabricUrls;
        MinecraftArgs = minecraftArgs;
        FabricArgs = fabricArgs;
    }
}
