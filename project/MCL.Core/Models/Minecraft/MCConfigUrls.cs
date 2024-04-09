namespace MCL.Core.Models.Minecraft;

public class MCConfigUrls
{
    public string VersionManifest { get; set; } = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
    public string PistonVersionManifest { get; set; } = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
    public string MinecraftResources { get; set; } = "https://resources.download.minecraft.net";
    public string JavaRuntimeIndexUrl { get; set; } =
        "https://launchermeta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json";

    public MCConfigUrls() { }

    public MCConfigUrls(
        string versionManifest,
        string pistonVersionManifest,
        string minecraftResources,
        string javaRuntimeIndexUrl
    )
    {
        VersionManifest = versionManifest;
        PistonVersionManifest = pistonVersionManifest;
        MinecraftResources = minecraftResources;
        JavaRuntimeIndexUrl = javaRuntimeIndexUrl;
    }
}
