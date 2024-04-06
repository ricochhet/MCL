namespace MCL.Core.Models.Minecraft;

public class MCConfigUrls
{
    public string VersionManifest { get; set; } = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
    public string PistonVersionManifest { get; set; } = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
    public string MinecraftResources { get; set; } = "https://resources.download.minecraft.net";
    public string JavaRuntimeIndexUrl { get; set; } =
        "https://launchermeta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json";
    public string FabricVersionsIndex { get; set; } = "https://meta.fabricmc.net/v2/versions";
    public string FabricLoaderJarUrl { get; set; } =
        "https://maven.fabricmc.net/net/fabricmc/fabric-loader/{0}/fabric-loader-{0}.jar";
}
