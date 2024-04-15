using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Models.MinecraftQuilt;
using MCL.Core.Models.Modding;
using MCL.Core.Models.Paper;
using MCL.Core.Models.SevenZip;

namespace MCL.Core.Models.Launcher;

public class Config
{
    public MCLauncherUsername LauncherUsername { get; set; }
    public MCLauncherPath LauncherPath { get; set; }
    public MCLauncherVersion LauncherVersion { get; set; }
    public MCLauncherSettings LauncherSettings { get; set; }
    public MCConfigUrls MinecraftUrls { get; set; }
    public MCFabricConfigUrls FabricUrls { get; set; }
    public MCQuiltConfigUrls QuiltUrls { get; set; }
    public PaperConfigUrls PaperUrls { get; set; }
    public JvmArguments MinecraftArgs { get; set; }
    public JvmArguments FabricArgs { get; set; }
    public JvmArguments QuiltArgs { get; set; }
    public JavaConfig JavaConfig { get; set; }
    public SevenZipConfig SevenZipConfig { get; set; }
    public ModConfig ModConfig { get; set; }

    public Config() { }
}
