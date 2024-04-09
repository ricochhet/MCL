using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Services.Modding;

namespace MCL.Core.Models;

public class Config
{
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }
    public JvmArguments MinecraftArgs { get; set; }
    public JvmArguments FabricArgs { get; set; }
    public JavaConfig JavaConfig { get; set; }
    public ModConfig ModConfig { get; set; }

    public Config() { }

    public Config(
        MCConfigUrls minecraftUrls,
        MCFabricConfigUrls fabricUrls,
        JvmArguments minecraftArgs,
        JvmArguments fabricArgs,
        JavaConfig javaConfig,
        ModConfig modConfig
    )
    {
        MinecraftUrls = minecraftUrls;
        FabricUrls = fabricUrls;
        MinecraftArgs = minecraftArgs;
        FabricArgs = fabricArgs;
        JavaConfig = javaConfig;
        ModConfig = modConfig;
    }
}
