using System.Text.Json.Serialization;
using MCL.Core.FileExtractors.Models;
using MCL.Core.Java.Models;
using MCL.Core.Minecraft.Models;
using MCL.Core.Modding.Models;
using MCL.Core.ModLoaders.Fabric.Models;
using MCL.Core.ModLoaders.Quilt.Models;
using MCL.Core.Servers.Paper.Models;

namespace MCL.Core.Launcher.Models;

public class Settings
{
    public LauncherInstance LauncherInstance { get; set; }

    [JsonPropertyName("LauncherInstance.Override")]
    public LauncherInstance OverrideLauncherInstance { get; set; }

    public LauncherUsername LauncherUsername { get; set; }
    public LauncherPath LauncherPath { get; set; }
    public LauncherVersion LauncherVersion { get; set; }
    public LauncherSettings LauncherSettings { get; set; }
    public MUrls MUrls { get; set; }
    public FabricUrls FabricUrls { get; set; }
    public QuiltUrls QuiltUrls { get; set; }
    public PaperUrls PaperUrls { get; set; }

    public JvmArguments MJvmArguments { get; set; }

    [JsonPropertyName("MJvmArguments.Override")]
    public JvmArguments OverrideMJvmArguments { get; set; }

    public JvmArguments FabricJvmArguments { get; set; }

    [JsonPropertyName("FabricJvmArguments.Override")]
    public JvmArguments OverrideFabricJvmArguments { get; set; }

    public JvmArguments QuiltJvmArguments { get; set; }

    [JsonPropertyName("QuiltJvmArguments.Override")]
    public JvmArguments OverrideQuiltJvmArguments { get; set; }

    public JavaSettings JavaSettings { get; set; }
    public SevenZipSettings SevenZipSettings { get; set; }

    public ModSettings ModSettings { get; set; }

    [JsonPropertyName("ModSettings.Override")]
    public ModSettings OverrideModSettings { get; set; }

    public Settings() { }
}
