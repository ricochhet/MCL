using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.Modding;
using MCL.Core.Models.ModLoaders.Fabric;
using MCL.Core.Models.ModLoaders.Quilt;
using MCL.Core.Models.Paper;
using MCL.Core.Models.SevenZip;

namespace MCL.Core.Models.Launcher;

public class Settings
{
    public LauncherUsername LauncherUsername { get; set; }
    public LauncherPath LauncherPath { get; set; }
    public LauncherVersion LauncherVersion { get; set; }
    public LauncherSettings LauncherSettings { get; set; }
    public MinecraftUrls MinecraftUrls { get; set; }
    public FabricUrls FabricUrls { get; set; }
    public QuiltUrls QuiltUrls { get; set; }
    public PaperUrls PaperUrls { get; set; }
    public JvmArguments MinecraftArgs { get; set; }
    public JvmArguments FabricArgs { get; set; }
    public JvmArguments QuiltArgs { get; set; }
    public JavaSettings JavaSettings { get; set; }
    public SevenZipSettings SevenZipSettings { get; set; }
    public ModSettings ModSettings { get; set; }

    public Settings() { }
}
